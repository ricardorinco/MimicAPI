using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi.Middlewares
{
    public static class ApiVersioning
    {
        public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
        {
            services.AddApiVersioning(cfg =>
            {
                cfg.ReportApiVersions = true;
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
            });

            return services;
        }
    }
}
