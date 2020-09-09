using Mimic.Domain.Models;
using System;

namespace Mimic.WebApi.Arguments.Dtos.Words
{
    public class QueryWordResponseDto
    {
        public int Id { get; set; }
        public bool Active { get; set; }

        public string Mimic { get; set; }
        public int Points { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static explicit operator QueryWordResponseDto(Word word)
        {
            return new QueryWordResponseDto()
            {
                Id = word.Id,
                Active = word.Active,

                Mimic = word.Description,
                Points = word.Points,

                CreatedAt = word.CreatedAt,

                UpdatedAt = word.UpdatedAt
            };
        }
    }
}
