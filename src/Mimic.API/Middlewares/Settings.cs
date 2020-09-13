using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi.Middlewares
{
    /// <summary>
    /// Implementação das Configurações
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Adiciona as configurações de Settings ao serviço
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddSettingesServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MimicApiSettings>(configuration);
            services.AddSingleton(configuration);

            return services;
        }
    }
}
