using Microsoft.AspNetCore.Mvc;

namespace Mimic.WebApi.V2.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/words")]
    [ApiVersion("2")]
    public class WordsController : ControllerBase
    {
        [HttpGet("", Name = "GetAllBySearch")]
        public IActionResult GetAll()
        {
            return Ok("Versão 2.0");
        }
    }
}
