using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserCardController : ControllerBase
    {
        private readonly IUserCardRepository _userCardRepository;
        public UserCardController(IUserCardRepository userCardRepository) {
            _userCardRepository = userCardRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToCard(Guid userId , Int64 cardId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString())) return BadRequest();
                var user_card = await _userCardRepository.AddUserToCardAsync(userId , cardId);
                return StatusCode(StatusCodes.Status201Created, new { message  = "Created" , data = user_card});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{userId}/{cardId}")]
        public async Task<IActionResult> RemoveUserFromCard(Guid userId, Int64 cardId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString())) return BadRequest();
                 await _userCardRepository.RemoveUserFromCardAsync(userId, cardId);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetListUserInCard(Guid userId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString())) return BadRequest();
                var user_cards = await _userCardRepository.GetListUserInCardAsync(userId);
                return Ok(user_cards);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
