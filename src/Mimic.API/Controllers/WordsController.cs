using Microsoft.AspNetCore.Mvc;
using Mimic.WebApi.Database.DataContext;
using Mimic.WebApi.Models;
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

        [Route("")]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(mimicContext.Words);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            var foundWord = mimicContext.Words.FirstOrDefault(x => x.Id == id);

            if (foundWord == null)
                return NotFound();

            return Ok(foundWord);
        }

        [Route("")]
        [HttpPost]
        public IActionResult Add([FromBody] Word word)
        {
            word.CreatedAt = DateTime.Now;
            mimicContext.Words.Add(word);
            mimicContext.SaveChanges();

            return Created($"api/words/{word.Id}", word);
        }

        [Route("{id}")]
        [HttpPut]
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

        [Route("{id}")]
        [HttpDelete]
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
