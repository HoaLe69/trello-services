using trello_services.Entities;

namespace trello_services.IRepository
{
    public interface IUserOfWorkspaceRepository
    {
        Task<UserWorkspace> AddUserToWorkSpace(Guid userId , Guid workspaceId , Role? role);
        void RemoveUserFromWorkSpace(Guid userId , Guid workspaceId);
        void UpdateRoleOfUser(Guid userId , Guid workspaceId);
        Task<UserWorkspace> FindByUserAndWorkspaceId(Guid userId , Guid workspaceId);
    }
}
