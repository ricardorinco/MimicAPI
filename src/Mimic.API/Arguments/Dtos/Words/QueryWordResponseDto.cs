using Mimic.Domain.Models;
using System;

namespace Mimic.WebApi.Arguments.Dtos.Words
{
    /// <summary>
    /// Classe de resposta para uma pesquisa realizada de palavras
    /// </summary>
    public class QueryWordResponseDto
    {
        /// <summary>
        /// Id da palavra
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Status da palavra
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Descrição da palavra
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Pontuação da palavra
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Data de criação da palavra
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Data de atualização da palavra
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Convesor explícito de QueryWordResponseDto para Word
        /// </summary>
        /// <param name="word">Word</param>
        public static explicit operator QueryWordResponseDto(Word word)
        {
            return new QueryWordResponseDto()
            {
                Id = word.Id,
                Active = word.Active,

                Description = word.Description,
                Points = word.Points,

                CreatedAt = word.CreatedAt,

                UpdatedAt = word.UpdatedAt
            };
        }
    }
}
