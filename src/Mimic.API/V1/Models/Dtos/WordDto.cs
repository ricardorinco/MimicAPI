using Mimic.Domain.Models;

namespace Mimic.WebApi.V1.Models.Dtos
{
    public class WordDto : BaseDto
    {
        public string Mimic { get; set; }
        public int Points { get; set; }

        public static explicit operator WordDto(Word word)
        {
            return new WordDto()
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
