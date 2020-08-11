using Microsoft.AspNetCore.Mvc;
using Mimic.WebApi.Database.DataContext;
using Mimic.WebApi.Models;
using System;
using System.Linq;

namespace Mimic.WebApi.Controllers
{
    [ApiController]
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
            return Ok(mimicContext.Words.Where(x => x.Id == id));
        }

        [Route("")]
        [HttpPost]
        public IActionResult Add([FromBody]Word word)
        {
            word.CreatedAt = DateTime.Now;
            mimicContext.Words.Add(word);
            mimicContext.SaveChanges();

            return Ok();
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult Update(int id, [FromBody]Word word)
        {
            word.Id = id;
            word.UpdatedAt = DateTime.Now;
            mimicContext.Words.Update(word);
            mimicContext.SaveChanges();

            return Ok();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var word = mimicContext.Words.Find(id);
            word.Active = false;
            mimicContext.Remove(mimicContext.Words.Find(id));

            return Ok();
        }
    }
}
