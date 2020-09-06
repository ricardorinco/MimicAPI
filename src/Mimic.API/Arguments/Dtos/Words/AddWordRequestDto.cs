namespace Mimic.WebApi.Arguments.Dtos.Words
{
    /// <summary>
    /// Classe para realizar a inclusão de uma nova palavra
    /// </summary>
    public class AddWordRequestDto
    {
        /// <summary>
        /// Descrição da palavra
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Pontuação da palavra
        /// </summary>
        public int Points { get; set; }
    }
}
