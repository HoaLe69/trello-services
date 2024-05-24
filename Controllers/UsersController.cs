using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) 
                    return BadRequest(new {message = "User is not exists"});
                var user = await _userRepository.GetByIdAsync(id);
                return Ok(new
                {
                    message = "success",
                    data = user
                });
            }
            catch {
                return StatusCode(StatusCodes.Status500InternalServerError);       
            }
        }

        
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(UserRequestModel request , Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString()))
                    return BadRequest(new { message = "User is not exists" });
                var userUpdated = await _userRepository.UpdateUserAsync(request , id);
                return Ok(new
                {
                    message = "success",
                    data = userUpdated
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
