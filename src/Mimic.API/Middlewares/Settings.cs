using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi.Middlewares
{
    public static class Settings
    {
        public static IServiceCollection AddSettingesServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MimicApiSettings>(configuration);
            services.AddSingleton(configuration);

            return services;
        }
    }
}
