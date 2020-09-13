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
    /// <summary>
    /// Implementação do serviço de gerador do Swagger
    /// </summary>
    public static class Swagger
    {
        /// <summary>
        /// Adiciona o gerador do Swagger ao serviço definindo um ou mais documentos swagger
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.Get<MimicApiSettings>();

            // TODO: Copiar XML Comments para o docker
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var applicationName = $"{PlatformServices.Default.Application.ApplicationName}.xml";
            var xmlCommentsPath = Path.Combine(applicationBasePath, applicationName);

            if (settings.SwaggerEnabled)
            {
                services.AddSwaggerGen(options =>
                {
                    options.ResolveConflictingActions(apiDescription => apiDescription.First());
                    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Mimic API", Version = "v2" });
                    options.SwaggerDoc("v1.1", new OpenApiInfo { Title = "Mimic API", Version = "v1.1" });
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Mimic API", Version = "v1" });
                    options.IncludeXmlComments(xmlCommentsPath);
                    options.DocInclusionPredicate((docName, apiDesc) =>
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
                    options.OperationFilter<RemoveVersionParameterFilter>();
                    options.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                });
            }

            return services;
        }

        /// <summary>
        /// Adiciona o gerador do Swagger à aplicação definindo um ou mais documentos swagger
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IApplicationBuilder</returns>
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
