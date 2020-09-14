using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mimic.Infra.Data.DataContext;

namespace Mimic.Infra.Data.Configuration
{
    public static class StartupData
    {
        /// <summary>
        /// Adiciona o contexto ao serviço
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<MimicContext>();

            return services;
        }

        /// <summary>
        /// Adiciona o Service Scope à aplicação para prover o AutoMigration
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="autoMigrationEnabled">AutoMigration enabled</param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder AddDataApllication(this IApplicationBuilder app, bool autoMigrationEnabled)
        {
            using (
                var serviceScope = app.ApplicationServices
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope()
            )
            {
                using (var context = serviceScope.ServiceProvider.GetService<MimicContext>())
                {
                    context.Database.Migrate();
                }
            }

            return app;
        }
    }
}
