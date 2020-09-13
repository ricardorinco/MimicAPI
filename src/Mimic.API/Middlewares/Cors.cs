using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi.Middlewares
{
    /// <summary>
    /// Implementação do CORS
    /// </summary>
    public static class Cors
    {
        /// <summary>
        /// Adiciona as configurações de CORS ao serviço
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddCorsServices(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            return services;
        }

        /// <summary>
        /// Adiciona as configurações de CORS à aplicação
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder AddCorsApllication(this IApplicationBuilder app)
        {
            app.UseCors("AllowAnyOrigin");

            return app;
        }
    }
}
