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
    public class LabelController : ControllerBase
    {
        private readonly ILabelRepository _labelRepository;
        public LabelController(ILabelRepository labelRepository)
        {
            _labelRepository = labelRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateLabel(LabelRequestModel request)
        {
            try
            {
                if (request == null) return BadRequest();
                var label = await _labelRepository.CreateNewLabelAsync(request);
                return StatusCode(StatusCodes.Status201Created , new {message = "created" , data = label});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{labelId}")]
        public async Task<IActionResult> DeleteLabel(Guid labelId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(labelId.ToString())) return BadRequest();
                await _labelRepository.DeleteLabelAsync(labelId);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{labelId}")]
        public async Task<IActionResult> UpdateLabel (Guid labelId , LabelRequestModel request)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(labelId.ToString())) return BadRequest();
                var label = await _labelRepository.UpdateLabelAsync(labelId, request);
                return Ok(new {message = "success" , data = label});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }

    }
}
