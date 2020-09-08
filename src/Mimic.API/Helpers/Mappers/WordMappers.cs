using Mimic.Application.Arguments.Dtos.Words;
using Mimic.WebApi.Arguments.Dtos.Words;

namespace Mimic.WebApi.Helpers.Mappers
{
    /// <summary>
    /// Mapeamendo dos objetos de Request
    /// </summary>
    public class WordMappers
    {
        /// <summary>
        /// Mapeamento de AddWordRequestDto para AddWordDto
        /// </summary>
        /// <param name="dto">Objeto de AddWordRequestDto</param>
        /// <returns>Objeto de AddWordDto</returns>
        public static AddWordDto AddWordRequestDtoToAddWordDto(AddWordRequestDto dto)
        {
            return new AddWordDto
            {
                Description = dto.Description,
                Points = dto.Points
            };
        }

        /// <summary>
        /// Mapeamento de DeleteWordRequestDto para DeleteWordDto
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Objeto de DeleteWordDto</returns>
        public static DeleteWordDto DeleteWordRequestDtoToDeleteWordDto(int id)
        {
            return new DeleteWordDto
            {
                Id = id
            };
        }

        /// <summary>
        /// Mapeamento de QueryWordequestDto para QueryWordDto
        /// </summary>
        /// <param name="dto">Objeto de QueryWordequestDto</param>
        /// <returns>Objeto de QueryWordDto</returns>
        public static QueryWordDto QueryWordequestDtoToQueryWordDto(QueryWordRequestDto dto)
        {
            return new QueryWordDto
            {
                CreatedDate = dto.CreatedDate,
                CurrentPage = dto.CurrentPage,
                PageSize = dto.PageSize
            };
        }

        /// <summary>
        /// Mapeamento de UpdateWordRequestDto para UpdateWordDto
        /// </summary>
        /// <param name="dto">Objeto de UpdateWordRequestDto</param>
        /// <returns>Objeto de UpdateWordDto</returns>
        public static UpdateWordDto UpdateWordRequestDtoToUpdateWordDto(UpdateWordRequestDto dto)
        {
            return new UpdateWordDto
            {
                Id = dto.Id,
                Description = dto.Description,
                Points = dto.Points,
                Active = dto.Active
            };
        }
    }
}
