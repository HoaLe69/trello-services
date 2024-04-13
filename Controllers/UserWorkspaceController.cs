using Microsoft.AspNetCore.Mvc;
using trello_services.Entities;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserWorkspaceController : ControllerBase
    {
        private readonly IUserOfWorkspaceRepository _userOfWorkspaceRepository;
        public UserWorkspaceController(IUserOfWorkspaceRepository userOfWorkspaceRepository)
        {
            _userOfWorkspaceRepository = userOfWorkspaceRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToWorkspace(Guid userId , Guid workspaceId , Role? role)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString()) || !ValidGuid.IsValidGuid(workspaceId.ToString())) return BadRequest();
                var user_of_workspace = await _userOfWorkspaceRepository.AddUserToWorkSpace(userId , workspaceId , role);
                return Ok(user_of_workspace);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
