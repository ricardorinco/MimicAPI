using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mimic.Infra.CrossCutting.NativeInjector;
using Mimic.Infra.Data.Configuration;
using Mimic.WebApi.Middlewares;

namespace Mimic.WebApi
{
    /// <summary>
    /// Implementações iniciais dos Middlewares
    /// </summary>
    public static class StartupMiddlewares
    {
        /// <summary>
        /// Realiza as configurações dos middlewares no serviço
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="configuration">IConfiguration</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection ConfigureApllicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRegisterServices(configuration)
                .AddSettingesServices(configuration)
                .AddCorsServices()
                .AddApiVersioningServices()
                .AddSwaggerServices(configuration)
                .AddDataServices()
                .AddOptions()
                .AddResponseCompression()
                .AddControllers();

            return services;
        }

        /// <summary>
        /// Realiza as configurações dos middlewares na aplicação
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IWebHostEnvironment</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder ConfigureApllication(this IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment env)
        {
            app
                .AddCorsApllication()
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseAuthorization()
                .UseStatusCodePages()
                .AddDataApllication()
                .AddSwaggerApllication(configuration)
                .UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            return app;
        }
    }
}
