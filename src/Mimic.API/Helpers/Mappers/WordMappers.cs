using Mimic.Application.Dtos.Words;
using Mimic.WebApi.Dtos.Words;

namespace Mimic.WebApi.Helpers.Mappers
{
    public class WordMappers
    {
        public static AddWordRuleDto AddWordRequestDtoToAddWordRuleDto(AddWordRequestDto dto)
        {
            return new AddWordRuleDto
            {
                Description = dto.Description,
                Points = dto.Points
            };
        }

        public static UpdateWordRuleDto UpdateWordRequestDtoToUpdateWordRuleDto(UpdateWordRequestDto dto)
        {
            return new UpdateWordRuleDto
            {
                Id = dto.Id,
                Description = dto.Description,
                Points = dto.Points,
                Active = dto.Active
            };
        }
    }
}
