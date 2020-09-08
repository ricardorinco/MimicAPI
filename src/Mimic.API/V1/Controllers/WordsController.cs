using Microsoft.AspNetCore.Mvc;
using Mimic.Application.Interfaces;
using Mimic.WebApi.Arguments.Dtos.Words;
using Mimic.WebApi.Helpers.Mappers;
using System.Net;
using System.Threading.Tasks;

namespace Mimic.WebApi.V1.Controllers
{
    /// <summary>
    /// Controller de palavras
    /// 
    /// Versões: v1.0 e 1.1
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/words")]
    [ApiVersion("1", Deprecated = true)]
    [ApiVersion("1.1")]
    public class WordsController : ControllerBase
    {
        private readonly IWordService wordService;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="wordService">Implementação de IWordService</param>
        public WordsController(IWordService wordService)
        {
            this.wordService = wordService;
        }

        /// <summary>
        /// Realiza a busca de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Palavra encontrada</returns>
        [HttpGet("{id}", Name = "GetWord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var foundWord = await wordService.GetByIdAsync(id);
            if (foundWord == null)
                return NotFound();

            return Ok(foundWord);
        }

        /// <summary>
        /// Realiza a inclusão da palavra informada
        /// </summary>
        /// <param name="requestDto">Objeto AddWordRequestDto</param>
        /// <returns>Objeto de Palavra</returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> AddAsync([FromBody] AddWordRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest();
            }

            var ruleDto = WordMappers.AddWordRequestDtoToAddWordDto(requestDto);
            var result = await wordService.AddAsync(ruleDto);

            if (!result.CustomValidation.IsValid)
            {
                return BadRequest(result.CustomValidation.Errors);
            }

            return Created($"api/words/{result.Entity.Id}", result.Entity);
        }

        /// <summary>
        /// Realiza a atualização da palavra informada
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <param name="requestDto">Objeto UpdateWordRequestDto</param>
        /// <returns>Palavra atualizada</returns>
        [HttpPut("{id}", Name = "UpdateWord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateWordRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest();
            }

            requestDto.Id = id;
            var ruleDto = WordMappers.UpdateWordRequestDtoToUpdateWordDto(requestDto);
            var result = await wordService.UpdateAsync(ruleDto);

            if (!result.CustomValidation.IsValid)
            {
                if (result.CustomValidation.Errors[0].ErrorCode == "DataNotFoundDatabase")
                {
                    return NotFound(result.CustomValidation.Errors);
                }
                
                return BadRequest(result.CustomValidation.Errors);
            }

            return Ok(result.Entity);
        }

        /// <summary>
        /// Realiza a exclusão de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        [HttpDelete("{id}", Name = "DeleteWord")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var ruleDto = WordMappers.DeleteWordRequestDtoToDeleteWordDto(id);
            var result = await wordService.DeleteAsync(ruleDto);

            if (!result.CustomValidation.IsValid)
            {
                if (result.CustomValidation.Errors[0].ErrorCode == "DataNotFoundDatabase")
                {
                    return NotFound(result.CustomValidation.Errors);
                }

                return BadRequest(result.CustomValidation.Errors);
            }

            return NoContent();
        }
    }
}
