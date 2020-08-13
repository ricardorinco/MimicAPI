using Microsoft.AspNetCore.Mvc;
using Mimic.WebApi.Database.DataContext;
using Mimic.WebApi.Helpers;
using Mimic.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Mimic.WebApi.Controllers
{
    [ApiController]
    [Route("api/words")]
    public class WordsController : ControllerBase
    {
        private readonly MimicContext mimicContext;

        public WordsController(MimicContext mimicContext)
        {
            this.mimicContext = mimicContext;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] WordUrlQuery query)
        {
            var words = mimicContext.Words.AsQueryable();
            if (query.SearchDate.HasValue)
            {
                words = words.Where(x => x.CreatedAt > query.SearchDate);
            }

            if (query.Page.HasValue)
            {
                var totalData = words.Count();
                words = words.Skip((query.Page.Value - 1) * query.DataAmount.Value).Take(query.DataAmount.Value);
                var pagination = new Pagination()
                {
                    Number = query.Page.Value,
                    Total = (int)Math.Ceiling((double)totalData / query.DataAmount.Value),

                    DataPerPage = query.DataAmount.Value,
                    TotalData = totalData
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

                if (query.Page.Value > pagination.Total)
                {
                    return NotFound();
                }
            }

            return Ok(words);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var foundWord = mimicContext.Words.FirstOrDefault(x => x.Id == id);

            if (foundWord == null)
                return NotFound();

            return Ok(foundWord);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Word word)
        {
            word.CreatedAt = DateTime.Now;
            mimicContext.Words.Add(word);
            mimicContext.SaveChanges();

            return Created($"api/words/{word.Id}", word);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Word word)
        {
            var foundWord = mimicContext.Words.FirstOrDefault(x => x.Id == id);

            if (foundWord == null)
                return NotFound();

            foundWord.Id = id;
            foundWord.UpdatedAt = DateTime.Now;
            mimicContext.Words.Update(foundWord);
            mimicContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var foundWord = mimicContext.Words.Find(id);
            if (foundWord == null)
                return NotFound();

            foundWord.Active = false;
            mimicContext.Words.Update(foundWord);
            mimicContext.SaveChanges();

            return Ok();
        }
    }
}
