using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Mimic.WebApi
{
    /// <summary>
    /// Implementa��o do host para a aplica��o
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Inicializa��o da cria��o do host
        /// </summary>
        /// <param name="args">args</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria��o do host com as configura��es necess�rias
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
