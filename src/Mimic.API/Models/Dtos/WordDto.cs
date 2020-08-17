using Mimic.WebApi.Helpers;

namespace Mimic.WebApi.Models.Dtos
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

                Mimic = word.Mimic,
                Points = word.Points,

                CreatedAt = word.CreatedAt,

                UpdatedAt = word.UpdatedAt
            };
        }
    }
}
