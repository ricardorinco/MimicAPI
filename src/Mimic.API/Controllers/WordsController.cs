using Microsoft.AspNetCore.Mvc;
using Mimic.WebApi.Database.DataContext;
using Mimic.WebApi.Models;
using System.Linq;

namespace Mimic.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly MimicContext mimicContext;

        public WordsController(MimicContext mimicContext)
        {
            this.mimicContext = mimicContext;
        }

        public IActionResult GetAll()
        {
            return Ok(mimicContext.Words);
        }

        public IActionResult Get(int id)
        {
            return Ok(mimicContext.Words.Where(x => x.Id == id));
        }

        public IActionResult Add(Word word)
        {
            mimicContext.Words.Add(word);

            return Ok();
        }

        public IActionResult Update(Word word)
        {
            mimicContext.Words.Update(word);

            return Ok();
        }

        public IActionResult Delete(int id)
        {
            mimicContext.Remove(mimicContext.Words.Find(id));

            return Ok();
        }
    }
}
