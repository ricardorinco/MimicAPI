using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mimic.Application.Interfaces;
using Mimic.Application.Services;
using Mimic.Infra.Data.Interfaces;
using Mimic.Infra.Data.Repositories;

namespace Mimic.Infra.CrossCutting.NativeInjector
{
    public static class NativeInjector
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Services
            services.AddScoped<IWordService, WordService>();

            // Repositories
            services.AddScoped<IWordRepository, WordRepository>();

            return services;
        }
    }
}
