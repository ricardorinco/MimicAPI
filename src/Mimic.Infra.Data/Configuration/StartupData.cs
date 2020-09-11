using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Mimic.Infra.Data.DataContext;

namespace Mimic.Infra.Data.Configuration
{
    public static class StartupData
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddDbContext<MimicContext>();

            return services;
        }

        public static IApplicationBuilder AddDataApllication(this IApplicationBuilder app)
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
