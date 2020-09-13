using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi
{
    /// <summary>
    /// Implementações iniciais da aplicação
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// IConfiguration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="configuration">Implementação de IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Realiza as configurações dos middlewares no serviço
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApllicationServices(Configuration);
        }

        /// <summary>
        /// Realiza as configurações dos middlewares na aplicação
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureApllication(Configuration, env);

            
        }
    }
}
