using Microsoft.AspNetCore.Mvc;
using Mimic.WebApi.Helpers;
using Mimic.WebApi.Models;
using Mimic.WebApi.Models.Dtos;
using Mimic.WebApi.Repository.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mimic.WebApi.Controllers
{
    [ApiController]
    [Route("api/words")]
    public class WordsController : ControllerBase
    {
        private readonly IWordRepository wordRepository;

        public WordsController(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
        }

        [HttpGet("", Name = "GetAllBySearch")]
        public IActionResult GetAllBySearch([FromQuery] WordUrlQuery query)
        {
            query.Page = query.Page.HasValue ? query.Page.Value : 1;
            query.DataAmount = query.DataAmount.HasValue ? query.DataAmount.Value : 10;

            var words = wordRepository.Get(query);

            if (words.Results.Count == 0)
            {
                return NotFound();
            }

            var workPaginationDto = new PaginationList<WordDto>();
            foreach (var word in words.Results)
            {
                var wordDto = (WordDto)word;
                wordDto.Links = new List<LinkDto>();
                wordDto.Links.Add(new LinkDto("self", Url.Link("GetWord", new { id = wordDto.Id }), "GET"));
                wordDto.Links.Add(new LinkDto("update", Url.Link("UpdateWord", new { id = wordDto.Id }), "PUT"));
                wordDto.Links.Add(new LinkDto("delete", Url.Link("DeleteWord", new { id = wordDto.Id }), "DELETE"));
                
                workPaginationDto.Results.Add(wordDto);
            }

            if (words.Pagination != null)
            {
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.Pagination));

                if (query.Page + 1 <= words.Pagination.Total)
                {
                    var queryString = new WordUrlQuery() { Page = query.Page + 1, DataAmount = query.DataAmount, SearchDate = query.SearchDate };
                    workPaginationDto.Links.Add(new LinkDto("next", Url.Link("GetAllBySearch", queryString), "GET"));
                }

                if (query.Page + 1 > 0)
                {
                    var queryString = new WordUrlQuery() { Page = query.Page - 1, DataAmount = query.DataAmount, SearchDate = query.SearchDate };
                    workPaginationDto.Links.Add(new LinkDto("previous", Url.Link("GetAllBySearch", queryString), "GET"));
                }

            }

            return Ok(workPaginationDto);
        }

        [HttpGet("{id}", Name = "GetWord")]
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

        [HttpPost]
        public IActionResult Add([FromBody] Word word)
        {
            wordRepository.Add(word);

            return Created($"api/words/{word.Id}", word);
        }

        [HttpPut("{id}", Name = "UpdateWord")]
        public IActionResult Update(int id, [FromBody] Word word)
        {
            var foundWord = wordRepository.GetById(id);

            if (foundWord == null)
                return NotFound();

            foundWord.Id = id;
            wordRepository.Update(foundWord);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteWord")]
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
