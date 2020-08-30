using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Mimic.Application.Interfaces;
using Mimic.Application.Services;
using Mimic.Infra.Data.DataContext;
using Mimic.Infra.Data.Interfaces;
using Mimic.Infra.Data.Repositories;
using Mimic.WebApi.Helpers.Swagger;
using System.IO;
using System.Linq;

namespace Mimic.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.Configure<MimicApiSettings>(Configuration);

            var settings = Configuration.Get<MimicApiSettings>();

            services.AddSingleton(Configuration);

            services.AddControllers();
            services.AddDbContext<MimicContext>();
            services.AddApiVersioning(cfg =>
            {
                cfg.ReportApiVersions = true;
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
            });

            var caminhoProjeto = PlatformServices.Default.Application.ApplicationBasePath;
            var nomeProjeto = $"{PlatformServices.Default.Application.ApplicationName}.xml";
            var caminhoArquivo = Path.Combine(caminhoProjeto, nomeProjeto);

            services.AddScoped<IWordRepository, WordRepository>();
            services.AddScoped<IWordService, WordService>();

            if (settings.SwaggerEnabled)
            {
                services.AddSwaggerGen(cfg =>
                {
                    cfg.ResolveConflictingActions(apiDescription => apiDescription.First());
                    cfg.SwaggerDoc("v2", new OpenApiInfo { Title = "Mimic API", Version = "v2" });
                    cfg.SwaggerDoc("v1.1", new OpenApiInfo { Title = "Mimic API", Version = "v1.1" });
                    cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimic API", Version = "v1" });
                    cfg.IncludeXmlComments(caminhoArquivo);
                    cfg.DocInclusionPredicate((docName, apiDesc) =>
                    {
                        var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                        if (actionApiVersionModel == null)
                        {
                            return true;
                        }

                        if (actionApiVersionModel.DeclaredApiVersions.Any())
                        {
                            return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v}" == docName);
                        }

                        return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v}" == docName);
                    });
                    cfg.OperationFilter<RemoveVersionParameterFilter>();
                    cfg.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                });
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            var settings = Configuration.Get<MimicApiSettings>();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddEnvironmentVariables()
                .Build();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStatusCodePages();

            if (settings.SwaggerEnabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI(cfg => {
                    cfg.SwaggerEndpoint("/swagger/v2/swagger.json", "Mimic API - v2");
                    cfg.SwaggerEndpoint("/swagger/v1.1/swagger.json", "Mimic API - v1.1");
                    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "Mimic API - v1");
                    cfg.RoutePrefix = string.Empty;
                });
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<MimicContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
