using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("")]
    public class IndexController : ControllerBase
    {
        public IndexController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, this is index! Our page is under the development rn, please be patient. It will be here in no time :D");
        }
    }
}
