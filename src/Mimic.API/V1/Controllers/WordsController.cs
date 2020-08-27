using Microsoft.AspNetCore.Mvc;
using Mimic.Application.Interfaces;
using Mimic.Domain.Arguments;
using Mimic.WebApi.Dtos.Words;
using Mimic.WebApi.Helpers.Mappers;
using Mimic.WebApi.V1.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimic.WebApi.V1.Controllers
{
    /// <summary>
    /// Controller de palavras
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
        /// Realiza uma consulta de palavras de acordo com os filtros informados
        /// </summary>
        /// <param name="requestDto">Objeto QueryWordRequestDto</param>
        /// <returns>Lista das palavras encontradas</returns>
        [HttpGet("", Name = "GetAllBySearch")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> GetAllBySearch([FromQuery] QueryWordRequestDto requestDto)
        {
            var ruleDto = WordMappers.QueryWordequestDtoToQueryWordRuleDto(requestDto);
            var words = await wordService.GetByQueryAsync(ruleDto);

            if (words.Count == 0)
            {
                return NotFound();
            }

            var wordsDto = new List<WordDto>();
            foreach (var word in words)
            {
                var wordDto = (WordDto)word;
                wordDto.Links = new List<Link>();
                wordDto.Links.Add(new Link("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));
                wordDto.Links.Add(new Link("update", Url.Link("UpdateWord", new { id = wordDto.Id }), "PUT"));
                wordDto.Links.Add(new Link("delete", Url.Link("DeleteWord", new { id = wordDto.Id }), "DELETE"));

                wordsDto.Add(wordDto);
            }

            return Ok(wordsDto);
        }

        /// <summary>
        /// Realiza a busca de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Palavra encontrada</returns>
        [HttpGet("{id}", Name = "GetWord")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var foundWord = await wordService.GetByIdAsync(id);

            if (foundWord == null)
                return NotFound();

            var wordDto = (WordDto)foundWord;
            wordDto.Links = new List<Link>();
            wordDto.Links.Add(new Link("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));
            wordDto.Links.Add(new Link("update", Url.Link("UpdateWord", new { id = wordDto.Id }), "PUT"));
            wordDto.Links.Add(new Link("delete", Url.Link("DeleteWord", new { id = wordDto.Id }), "DELETE"));

            return Ok(wordDto);
        }

        /// <summary>
        /// Realiza a inclusão da palavra informada
        /// </summary>
        /// <param name="requestDto">Objeto AddWordRequestDto</param>
        /// <returns>Objeto de Palavra</returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> AddAsync([FromBody] AddWordRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var ruleDto = WordMappers.AddWordRequestDtoToAddWordRuleDto(requestDto);
            var word = await wordService.AddAsync(ruleDto);

            // word.Links.Add(new Link("self", Url.Link("GetWord", new { id = word.Id }), "GET"));

            return Created($"api/words/{word.Id}", word);
        }

        /// <summary>
        /// Realiza a atualização da palavra informada
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <param name="requestDto">Objeto UpdateWordRequestDto</param>
        /// <returns>Palavra atualizada</returns>
        [HttpPut("{id}", Name = "UpdateWord")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateWordRequestDto requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest();
            }

            requestDto.Id = id;
            var ruleDto = WordMappers.UpdateWordRequestDtoToUpdateWordRuleDto(requestDto);
            var word = await wordService.UpdateAsync(ruleDto);

            // wordDto.Links.Add(new Link("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));

            return Ok(word);
        }

        /// <summary>
        /// Realiza a exclusão de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        [HttpDelete("{id}", Name = "DeleteWord")]
        [MapToApiVersion("1.1")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var ruleDto = WordMappers.DeleteWordRequestDtoToDeleteWordRuleDto(id);
            await wordService.DeleteAsync(ruleDto);

            return NoContent();
        }
    }
}
