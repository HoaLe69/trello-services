using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using trello_services.Entities;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/userboard")]
    [ApiController]
    public class UserBoardController : ControllerBase
    {
        private readonly IUserBoardRepository _userBoardRepository;
        public UserBoardController(IUserBoardRepository userBoardRepository)
        {
            _userBoardRepository = userBoardRepository;
        }
        [HttpGet("{boardId}")]
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
        [HttpPost]
        public async Task<IActionResult> Create(UserBoard userBoard)
        {
            try
            {
                if (userBoard == null) return BadRequest();
                  if(!(ValidGuid.IsValidGuid(userBoard.userId.ToString()) && 
                        ValidGuid.IsValidGuid(userBoard.boardId.ToString())))
                            return BadRequest();
                    var user = await _userBoardRepository.AddUserToBoardAsync(userBoard);
                    return Ok(user);
                
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        
    }
}
