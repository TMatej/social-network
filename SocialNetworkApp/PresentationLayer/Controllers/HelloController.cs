using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        public HelloController() { }

        [HttpGet]
        public string Get()
        {
            return "Hello!";
        }
    }
}
