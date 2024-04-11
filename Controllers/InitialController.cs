using Microsoft.AspNetCore.Mvc;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialController : ControllerBase
    {
        [HttpGet]
        public IActionResult greet()
        {
            return Ok(new
            {
                Message = "Hello Trello Service Initial"
            });
        }
    }
}
