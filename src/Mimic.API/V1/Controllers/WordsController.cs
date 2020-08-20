using Microsoft.AspNetCore.Mvc;
using Mimic.WebApi.Helpers;
using Mimic.WebApi.V1.Models;
using Mimic.WebApi.V1.Models.Dtos;
using Mimic.WebApi.V1.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mimic.WebApi.V1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/words")]
    [ApiVersion("1", Deprecated = true)]
    [ApiVersion("1.1")]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository wordRepository;

        public WordsController(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

        /// <summary>
        /// Obtêm todas as palavras
        /// </summary>
        /// <param name="query">Filtros de pesquisa</param>
        /// <returns>Lista de palavras</returns>
        [HttpGet("", Name = "GetAllBySearch")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult GetAllBySearch([FromQuery] WordUrlQuery query)
        {
            query.Page = query.Page.HasValue ? query.Page.Value : 1;
            query.DataAmount = query.DataAmount.HasValue ? query.DataAmount.Value : 10;

            var words = wordRepository.Get(query);

            if (words.Results.Count == 0)
            {
                return NotFound();
            }

            var wordPaginationDto = new PaginationList<WordDto>();
            foreach (var word in words.Results)
            {
                var wordDto = (WordDto)word;
                wordDto.Links = new List<LinkDto>();
                wordDto.Links.Add(new LinkDto("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));
                wordDto.Links.Add(new LinkDto("update", Url.Link("UpdateWord", new { id = wordDto.Id }), "PUT"));
                wordDto.Links.Add(new LinkDto("delete", Url.Link("DeleteWord", new { id = wordDto.Id }), "DELETE"));

                wordPaginationDto.Results.Add(wordDto);
            }

            if (words.Pagination != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.Pagination));

                if (query.Page + 1 <= words.Pagination.Total)
                {
                    var queryString = new WordUrlQuery() { Page = query.Page + 1, DataAmount = query.DataAmount, SearchDate = query.SearchDate };
                    wordPaginationDto.Links.Add(new LinkDto("next", Url.Link("GetAllBySearch", queryString), "GET"));
                }

                if (query.Page + 1 > 0)
                {
                    var queryString = new WordUrlQuery() { Page = query.Page - 1, DataAmount = query.DataAmount, SearchDate = query.SearchDate };
                    wordPaginationDto.Links.Add(new LinkDto("previous", Url.Link("GetAllBySearch", queryString), "GET"));
                }

            }

            return Ok(wordPaginationDto);
        }

        /// <summary>
        /// Obtêm uma palavra por id
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Objeto de Palavra</returns>
        [HttpGet("{id}", Name = "GetWord")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult Get(int id)
        {
            var foundWord = wordRepository.GetById(id);

            if (foundWord == null)
                return NotFound();

            var wordDto = (WordDto)foundWord;
            wordDto.Links = new List<LinkDto>();
            wordDto.Links.Add(new LinkDto("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));
            wordDto.Links.Add(new LinkDto("update", Url.Link("UpdateWord", new { id = wordDto.Id }), "PUT"));
            wordDto.Links.Add(new LinkDto("delete", Url.Link("DeleteWord", new { id = wordDto.Id }), "DELETE"));

            return Ok(wordDto);
        }

        /// <summary>
        /// Adiciona uma nova palavra
        /// </summary>
        /// <param name="word">Objeto de Palavra</param>
        /// <returns>Objeto de Palavra</returns>
        [HttpPost]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult Add([FromBody] Word word)
        {
            if (word == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            word.CreatedAt = DateTime.Now;
            word.Active = true;
            wordRepository.Add(word);

            var wordDto = (WordDto)word;
            wordDto.Links.Add(new LinkDto("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));

            return Created($"api/words/{word.Id}", wordDto);
        }

        /// <summary>
        /// Atualiza uma palavra
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <param name="word">Objeto de Palavra à atualizar</param>
        /// <returns>Objeto de Palavra atualizado</returns>
        [HttpPut("{id}", Name = "UpdateWord")]
        [MapToApiVersion("1.0")]
        [MapToApiVersion("1.1")]
        public IActionResult Update(int id, [FromBody] Word word)
        {
            var foundWord = wordRepository.GetById(id);

            if (foundWord == null)
                return NotFound();

            word.Id = foundWord.Id;
            word.CreatedAt = foundWord.CreatedAt;
            word.Active = foundWord.Active;
            word.UpdatedAt = DateTime.Now;
            wordRepository.Update(word);

            var wordDto = (WordDto)word;
            wordDto.Links.Add(new LinkDto("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));

            return Ok(wordDto);
        }

        /// <summary>
        /// Deleta uma palavra
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = "DeleteWord")]
        [MapToApiVersion("1.1")]
        public IActionResult Delete(int id)
        {
            var foundWord = wordRepository.GetById(id);

            if (foundWord == null)
                return NotFound();

            wordRepository.Delete(id);

            return NoContent();
        }
    }
}
