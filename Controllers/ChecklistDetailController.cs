using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChecklistDetailController : ControllerBase
    {
        private readonly IChecklistDetailRepository _repository;
        public ChecklistDetailController(IChecklistDetailRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateChecklist(string name , Guid clId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(clId.ToString())) return BadRequest();
                var cli = await _repository.CreateCheckListItemAsync(name, clId);
                return StatusCode(StatusCodes.Status201Created, new { message = "created", data = cli });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChecklistItem(Int64 id)
        {
            try
            {
                await _repository.DeleteListItemAsync(id);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{id}/update-content-task")]
        public async Task<IActionResult> UpdateNameChecklistItem(Int64 id, string name)
        {
            try
            {
                var update = await _repository.UpdateChecklistItemContentAsync(id , name);
                return StatusCode(StatusCodes.Status201Created, new { message = "Updated", data = update });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{id}/mark-completed-task")]
        public async Task<IActionResult> MarkItemCompleteOrNot(Int64 id, bool status)
        {
            try
            {
                await _repository.UpdateStatusOfListItemAsync(id, status);
                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("clId")]
        public async Task<IActionResult> GetListItemByClId(Guid  clId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(clId.ToString())) return BadRequest();
                var listItems = await _repository.GetAllCheckListDetailAsync(clId);
                return Ok(listItems);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
    