using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Mimic.WebApi.Helpers.Swagger;
using System.IO;
using System.Linq;

namespace Mimic.WebApi.Middlewares
{
    public static class Swagger
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.Get<MimicApiSettings>();

            // TODO: Copiar XML Comments para o docker
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = $"{PlatformServices.Default.Application.ApplicationName}.xml";
            var xmlCommentsPath = Path.Combine(applicationBasePath, applicationName);

            if (settings.SwaggerEnabled)
            {
                services.AddSwaggerGen(cfg =>
                {
                    cfg.ResolveConflictingActions(apiDescription => apiDescription.First());
                    cfg.SwaggerDoc("v2", new OpenApiInfo { Title = "Mimic API", Version = "v2" });
                    cfg.SwaggerDoc("v1.1", new OpenApiInfo { Title = "Mimic API", Version = "v1.1" });
                    cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimic API", Version = "v1" });
                    cfg.IncludeXmlComments(xmlCommentsPath);
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

            return services;
        }

        public static IApplicationBuilder AddSwaggerApllication(this IApplicationBuilder app, IConfiguration configuration)
        {
            var settings = configuration.Get<MimicApiSettings>();

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

            return app;
        }
    }
}
