using System.Data;
using trello_services.Entities;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IUserOfWorkspaceRepository
    {
        Task<UserWorkspace> AddUserToWorkSpaceAsync(Guid workspaceId, Guid userId , Role? role);
        Task RemoveUserFromWorkSpaceAsync(Guid userId , Guid workspaceId);
        Task<UserWorkspace> UpdateRoleOfUserAsync(Guid userId , Guid workspaceId , Role role);
        Task<UserWorkspace> FindByUserAndWorkspaceIdAsync(Guid userId , Guid workspaceId);
        Task<ICollection<UserWorkspace>> GetWorkSpaceOfUserByUserIdAsync(Guid userId);
        Task<IList<UserResponseVM>> GetMemberOfWorkspaceByWorkspaceIdAsync(Guid workspaceId);
    }
}
