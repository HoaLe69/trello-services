using Microsoft.AspNetCore.Mvc;
using trello_services.IRepository;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;
        private readonly IJwtServices _tokenService;
        public AuthController( IAuth auth , IJwtServices jwtServices)
        {
            _auth = auth;
            _tokenService = jwtServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginModel request)
        {
            try
            {
                var user = await _auth.AuthenticatedAsync(request);
                if (user == null)
                {
                    return BadRequest(new { Success = false, message = "Invalid email or password" });
                }
                var access_token = _tokenService.GenerateToken(user);
                return Ok(new { Success = true , Access_token = access_token});
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserLoginModel request)
        {
            try
            {
                var isSuccess = await _auth.RegisterAsync(request);
                if (isSuccess)
                {
                    return Ok(new { Success = true , message = "Register successfully" });
                }
                return BadRequest(new { Success = false , message = "Email already exist" });

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
