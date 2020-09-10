using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mimic.Infra.CrossCutting.NativeInjector;

namespace Mimic.WebApi.Middlewares
{
    public static class StartupMiddlewares
    {
        public static IServiceCollection ConfigureApllicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .RegisterServices(configuration)
                .AddCorsServices()
                .AddResponseCompression();

            return services;
        }
    }
}
