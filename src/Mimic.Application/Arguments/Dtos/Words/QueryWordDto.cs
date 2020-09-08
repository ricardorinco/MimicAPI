using System;

namespace Mimic.Application.Arguments.Dtos.Words
{
    /// <summary>
    /// Classe responsável por realização das consultas às palavras
    /// </summary>
    public class QueryWordDto
    {
        /// <summary>
        /// Data de criação da palavra para ser consultada, padrão será -30 dias
        /// contandos à partir da data de pesquisa
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Número da página atual, padrão será a primeira página
        /// </summary>
        public int? CurrentPage { get; set; }
        /// <summary>
        /// Número de itens por página, padrão será 10 itens
        /// </summary>
        public int? PageSize { get; set; }
    }
}
