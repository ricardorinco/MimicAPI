using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Mimic.WebApi
{
    /// <summary>
    /// Implementação do host para a aplicação
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Inicialização da criação do host
        /// </summary>
        /// <param name="args">args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Criação do host com as configurações necessárias
        /// </summary>
        /// <param name="args">args</param>
        /// <returns>IHostBuilder</returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
            
    }
}
