namespace Mimic.WebApi
{
    /// <summary>
    /// Configurações da aplicação
    /// </summary>
    public class MimicApiSettings
    {
        /// <summary>
        /// Propriedade de exibição do Swagger
        /// </summary>
        public bool SwaggerEnabled { get; set; }

        /// <summary>
        /// Propriedade de Migration da base de dados
        /// </summary>
        public bool AutoMigrationEnabled { get; set; }
    }
}
