using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using trello_services.Entities;
using trello_services.Helpers;
using trello_services.IRepository;

namespace trello_services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserWorkspaceController : ControllerBase
    {
        private readonly IUserOfWorkspaceRepository _userOfWorkspaceRepository;
        
        public UserWorkspaceController(IUserOfWorkspaceRepository userOfWorkspaceRepository)
        {
            _userOfWorkspaceRepository = userOfWorkspaceRepository;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserToWorkspace(Guid workspaceId, Guid userId , Role? role)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString()) ||
                    !ValidGuid.IsValidGuid(workspaceId.ToString())) 
                        return BadRequest();
                var user_of_workspace = await _userOfWorkspaceRepository.AddUserToWorkSpaceAsync(workspaceId, userId , role);
                return Ok(user_of_workspace);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{userId}/workspace-of-user")]
        public async Task<IActionResult> GetListWorkspaceOfUser(Guid userId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid(userId.ToString())) return BadRequest();
                var worksapce = await _userOfWorkspaceRepository.GetWorkSpaceOfUserByUserIdAsync(userId);
                return Ok(worksapce);
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpGet("{workspaceId}/member-of-workspace")]
        public async Task<IActionResult> GetListMemberFromWorkspace (Guid workspaceId)
        {
            try
            {
                if (!ValidGuid.IsValidGuid (workspaceId.ToString())) return BadRequest();
                   var members = await _userOfWorkspaceRepository.GetMemberOfWorkspaceByWorkspaceIdAsync(workspaceId);
                return Ok(members);

            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpPatch("{userId}/{workspaceId}")]
        public async Task<IActionResult> UpdateRoleOfUserInWorkspace(Guid userId , Guid workspaceId, Role role)
        {
            try
            {
                if (!(ValidGuid.IsValidGuid(userId.ToString()) && ValidGuid.IsValidGuid(workspaceId.ToString()))) 
                        return BadRequest();
                var user_worksapce_update = await _userOfWorkspaceRepository.UpdateRoleOfUserAsync(userId, workspaceId, role);
                return Ok(new { message = "Update successfully"  , data = user_worksapce_update});
            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
        [HttpDelete("{userId}/{workspaceId}")]
        public async Task<IActionResult> RemoveUserFromWorkspace(Guid userId , Guid workspaceId)
        {
            try
            {
                if (!(ValidGuid.IsValidGuid(userId.ToString()) && ValidGuid.IsValidGuid(workspaceId.ToString())))
                    return BadRequest();
                await _userOfWorkspaceRepository.RemoveUserFromWorkSpaceAsync(userId, workspaceId);
                return NoContent();

            }
            catch
            {
                return ResponseHelper.InternalServerError();
            }
        }
    }
}
