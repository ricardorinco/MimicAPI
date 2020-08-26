using Microsoft.AspNetCore.Mvc;
using Mimic.Application.Interfaces;
using Mimic.Domain.Arguments;
using Mimic.Infra.Data.Interfaces;
using Mimic.WebApi.Dtos.Words;
using Mimic.WebApi.Helpers;
using Mimic.WebApi.Helpers.Mappers;
using Mimic.WebApi.V1.Models.Dtos;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimic.WebApi.V1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/words")]
    [ApiVersion("1", Deprecated = true)]
    [ApiVersion("1.1")]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository wordRepository;
        private readonly IWordService wordService;

        public WordsController(
            IWordRepository wordRepository,
            IWordService wordService
        )
        {
            this.wordRepository = wordRepository;
            this.wordService = wordService;
        }

        /// <summary>
        /// Obtêm todas as palavras
        /// </summary>
        /// <param name="query">Filtros de pesquisa</param>
        /// <returns>Lista de palavras</returns>
        [HttpGet("", Name = "GetAllBySearch")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult GetAllBySearch([FromQuery] WordQuery query)
        {
            query.Page = query.Page.HasValue ? query.Page.Value : 1;
            query.DataAmount = query.DataAmount.HasValue ? query.DataAmount.Value : 10;

            var words = wordRepository.GetByQuery(query);

            if (words.Results.Count == 0)
            {
                return NotFound();
            }

            var wordPaginationDto = new PaginationList<WordDto>();
            foreach (var word in words.Results)
            {
                var wordDto = (WordDto)word;
                wordDto.Links = new List<Link>();
                wordDto.Links.Add(new Link("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));
                wordDto.Links.Add(new Link("update", Url.Link("UpdateWord", new { id = wordDto.Id }), "PUT"));
                wordDto.Links.Add(new Link("delete", Url.Link("DeleteWord", new { id = wordDto.Id }), "DELETE"));

                wordPaginationDto.Results.Add(wordDto);
            }

            if (words.Pagination != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.Pagination));

                if (query.Page + 1 <= words.Pagination.Total)
                {
                    var queryString = new WordUrlQuery() { Page = query.Page + 1, DataAmount = query.DataAmount, SearchDate = query.SearchDate };
                    wordPaginationDto.Links.Add(new Link("next", Url.Link("GetAllBySearch", queryString), "GET"));
                }

                if (query.Page + 1 > 0)
                {
                    var queryString = new WordUrlQuery() { Page = query.Page - 1, DataAmount = query.DataAmount, SearchDate = query.SearchDate };
                    wordPaginationDto.Links.Add(new Link("previous", Url.Link("GetAllBySearch", queryString), "GET"));
                }

            }

            return Ok(wordPaginationDto);
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
