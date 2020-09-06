using Mimic.Application.Arguments.Dtos.Words;
using Mimic.WebApi.Arguments.Dtos.Words;

namespace Mimic.WebApi.Helpers.Mappers
{
    /// <summary>
    /// Mapeamendo dos objetos de Request para Rules
    /// </summary>
    public class WordMappers
    {
        /// <summary>
        /// Mapeamento de AddWordRequestDto para AddWordRuleDto
        /// </summary>
        /// <param name="dto">Objeto de AddWordRequestDto</param>
        /// <returns>Objeto de AddWordRuleDto</returns>
        public static AddWordRuleDto AddWordRequestDtoToAddWordRuleDto(AddWordRequestDto dto)
        {
            return new AddWordRuleDto
            {
                Description = dto.Description,
                Points = dto.Points
            };
        }

        /// <summary>
        /// Mapeamento de DeleteWordRequestDto para DeleteWordRuleDto
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Objeto de DeleteWordRuleDto</returns>
        public static DeleteWordRuleDto DeleteWordRequestDtoToDeleteWordRuleDto(int id)
        {
            return new DeleteWordRuleDto
            {
                Id = id
            };
        }

        /// <summary>
        /// Mapeamento de QueryWordequestDto para QueryWordRuleDto
        /// </summary>
        /// <param name="dto">Objeto de QueryWordequestDto</param>
        /// <returns>Objeto de QueryWordRuleDto</returns>
        public static QueryWordRuleDto QueryWordequestDtoToQueryWordRuleDto(QueryWordRequestDto dto)
        {
            return new QueryWordRuleDto
            {
                CreatedDate = dto.CreatedDate,
                CurrentPage = dto.CurrentPage,
                PageSize = dto.PageSize
            };
        }

        /// <summary>
        /// Mapeamento de UpdateWordRequestDto para UpdateWordRuleDto
        /// </summary>
        /// <param name="dto">Objeto de UpdateWordRequestDto</param>
        /// <returns>Objeto de UpdateWordRuleDto</returns>
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
