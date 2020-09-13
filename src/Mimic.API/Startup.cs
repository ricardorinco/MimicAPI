using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mimic.WebApi
{
    /// <summary>
    /// Implementa��es iniciais da aplica��o
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
        /// <param name="configuration">Implementa��o de IConfiguration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Realiza as configura��es dos middlewares no servi�o
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureApllicationServices(Configuration);
        }

        /// <summary>
        /// Realiza as configura��es dos middlewares na aplica��o
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureApllication(Configuration, env);

            
        }
    }
}
