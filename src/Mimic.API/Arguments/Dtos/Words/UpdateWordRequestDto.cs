namespace Mimic.WebApi.Arguments.Dtos.Words
{
    /// <summary>
    /// Classe para realizar a atualização de uma palavra
    /// </summary>
    public class UpdateWordRequestDto
    {
        /// <summary>
        /// Id da palavra a ser atualizada
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nova descrição da palavra
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Nova pontuação da palavra
        /// </summary>
        public int Points { get; set; }
        /// <summary>
        /// Novo status da palavra
        /// </summary>
        public bool Active { get; set; }
    }
}
