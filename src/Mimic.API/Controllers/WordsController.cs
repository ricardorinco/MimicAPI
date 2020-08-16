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

        [HttpGet]
        public IActionResult GetAll([FromQuery] WordUrlQuery query)
        {
            query.Page = query.Page.HasValue ? query.Page.Value : 1;
            query.DataAmount = query.DataAmount.HasValue ? query.DataAmount.Value : 10;

            var words = wordRepository.Get(query);
            if (query.Page.Value > words.Pagination.Total)
            {
                return NotFound();
            }

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(words.Pagination));

            return Ok(words);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var foundWord = wordRepository.GetById(id);

            if (foundWord == null)
                return NotFound();

            var wordDto = (WordDto)foundWord;
            wordDto.Links = new List<LinkDto>();
            wordDto.Links.Add(
                new LinkDto
                (
                    "self",
                    $"https://localhost:44367/api/words/{wordDto.Id}",
                    "GET"
                )
            );

            return Ok(wordDto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Word word)
        {
            wordRepository.Add(word);

            return Created($"api/words/{word.Id}", word);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Word word)
        {
            var foundWord = wordRepository.GetById(id);

            if (foundWord == null)
                return NotFound();

            foundWord.Id = id;
            wordRepository.Update(foundWord);

            return NoContent();
        }

        [HttpDelete("{id}")]
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
