using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Entities;
using trello_services.Helpers;
using trello_services.IRepository;
using trello_services.Models.Request;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserBoardController : ControllerBase
    {
        private readonly IUserBoardRepository _userBoardRepository;
        public UserBoardController(IUserBoardRepository userBoardRepository)
        {
            _userBoardRepository = userBoardRepository;
        }
        [HttpGet("{boardId}/users")]
        public async Task<IActionResult> GetListUserOfBoard(Guid boardId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(boardId.ToString())) return BadRequest();
                var users = await _userBoardRepository.GetListUserOfBoardAsync(boardId);
                return Ok(new
                {
                    message = "success",
                    data = users,
                });

            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{userId}/guest-board")]
        public async Task<IActionResult> GetListGuestBoard(Guid userId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString())) return BadRequest();
                var boards = await _userBoardRepository.GetBoardByUserId(userId);
                return Ok(new
                {
                    message = "success",
                    data = boards,
                });

            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserBoardRequestModel request)
        {
            try
            {
                  if (request == null) return BadRequest();
                  if(!(ValidGuid.IsValidGuid(request.userId.ToString()) && 
                        ValidGuid.IsValidGuid(request.boardId.ToString())))
                            return BadRequest();
                    var user = await _userBoardRepository.AddUserToBoardAsync(request);
                    return Ok(new { success = true , data = user});
                
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{userId}/{boardId}")]
        public async Task<IActionResult> RemoveUserBoard(Guid userId , Guid boardId)
        {
            try
            {
                if (!(ValidGuid.IsValidGuid(userId.ToString()) &&
                        ValidGuid.IsValidGuid(boardId.ToString())))
                    return BadRequest();
                await _userBoardRepository.RemoveUserFromBoardAsync(userId, boardId);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{userId}/{boardId}")]
        public async Task<IActionResult> UpdateRoleOfUser(Guid userId , Guid boardId , Role role)
        {
            try
            {
                if (!(ValidGuid.IsValidGuid(userId.ToString()) &&
                        ValidGuid.IsValidGuid(boardId.ToString())))
                    return BadRequest();
                var user_update = await _userBoardRepository.UpdateRoleOfUserAsync(userId, boardId, role);
                return Ok(user_update);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{userId}/{boardId}/board-to-star-list")]
        public async Task<IActionResult> AddBoardToStarUserList(Guid userId, Guid boardId, bool star)
        {
            try
            {
                if (!(ValidGuid.IsValidGuid(userId.ToString()) &&
                        ValidGuid.IsValidGuid(boardId.ToString())))
                    return BadRequest();
                await _userBoardRepository.AddBoardToFavouriteListUserAsync(userId, boardId, star);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
