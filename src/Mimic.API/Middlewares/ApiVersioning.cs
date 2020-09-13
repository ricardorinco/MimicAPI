using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi.Middlewares
{
    /// <summary>
    /// Implementação do versionamento de api
    /// </summary>
    public static class ApiVersioning
    {
        /// <summary>
        /// Adiciona o leitor de versionamento de api ao serviço e configura as versões
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });

            return services;
        }
    }
}
