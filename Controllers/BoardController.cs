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
    public class BoardController : ControllerBase
    {
        private readonly IBoardRepository _boardRepository;
        public BoardController(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBoard(BoardRequestModel request)
        {
            try
            {
                if (request == null || request.workSpaceId == null) return BadRequest();
                return Ok(new { message = "Success", data = await _boardRepository.CreateNewBoardAsync(request) });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetDetailBoardById (Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) return BadRequest();
                var board = await _boardRepository.GetBoardDetailByID(id);
                return Ok(new {success = true , data = board});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        } 
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateBoard(Guid id , string title)
        {
            try 
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) return BadRequest(); 
                return Ok(new { message = "update successfully" , data = await _boardRepository.UpdateTitleBoardAsync(id , title) });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoard(Guid id)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) return BadRequest();
                await _boardRepository.DeleteBoardAsync(id);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{workspaceId}/list")]
        public async Task<IActionResult> GetAllBoardByWorkspaceId (Guid workspaceId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(workspaceId.ToString())) return BadRequest();
                var boards  = await _boardRepository.GetAllBoardsByWoskspaceIdAsync(workspaceId);
                return Ok(new {success = true , data = boards});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
