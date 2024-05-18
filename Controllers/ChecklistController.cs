using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChecklistController : ControllerBase
    {
        private readonly IChecklistRepository _repository;
        public ChecklistController(IChecklistRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateChecklist(Guid cardId , string name)
        {
            try
            {
                if (name == null) return BadRequest();
                var cl = await _repository.CreateCheckListAsync(name, cardId);
                return StatusCode(StatusCodes.Status201Created, new { message = "created", data = cl });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{clId}")]
        public async Task<IActionResult> DeleteChecklist(Guid clId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(clId.ToString())) return BadRequest();
                await _repository.DeleteCheckListAsync(clId);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{clId}")]
        public async Task<IActionResult> UpdateNameChecklist(Guid clId , string name)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(clId.ToString())) return BadRequest();
                var update = await _repository.UpdateNameCheckListAsync(clId , name);
                return StatusCode(StatusCodes.Status201Created, new { message = "Updated", data = update });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
