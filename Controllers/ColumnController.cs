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
    public class ColumnController : ControllerBase
    {
        private readonly IColumnRepository _columnRepository;
        public ColumnController(IColumnRepository columnRepository)
        {
            _columnRepository = columnRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewColumn(ListCardRequestModel request)
        {
            try
            {
                var column = await _columnRepository.CreateNewColumnAsync(request);
                return Ok(new
                {
                    message = "success",
                    data = column
                });
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTitleColumn (Guid id , string title)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(id.ToString())) return BadRequest();
                if (title == null) return BadRequest();
                await _columnRepository.UpdateTitleColumnAsync(id, title);
                return NoContent();
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
