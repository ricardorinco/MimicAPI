using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mimic.Infra.CrossCutting.NativeInjector;
using Mimic.Infra.Data.Configuration;
using Mimic.WebApi.Middlewares;

namespace Mimic.WebApi
{
    public static class StartupMiddlewares
    {
        public static IServiceCollection ConfigureApllicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterServices(configuration)
                .AddSettingesServices(configuration)
                .AddCorsServices()
                .AddApiVersioningServices()
                .AddSwaggerServices(configuration)
                .AddDataServices()
                .AddResponseCompression();

            return services;
        }

        public static IApplicationBuilder ConfigureApllication(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.AddCorsApllication()
                .AddDataApllication()
                .AddSwaggerApllication(configuration)
                .UseResponseCompression();

            return app;
        }
    }
}
