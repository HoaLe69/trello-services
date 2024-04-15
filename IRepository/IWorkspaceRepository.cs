using trello_services.Entities;
using trello_services.Models.Request;

namespace trello_services.IRepository
{
    public interface IWorkspaceRepository
    {
        Task<WorkSpace> CreateWorkspaceAsync(WorkspaceRequestModel request);
        Task<WorkSpace> UpdateWorkspaceAsync(WorkspaceRequestModel request , Guid id);
        Task DeleteWorkspaceAsync(Guid workspaceId);
        Task<WorkSpace> GetWorkspaceByIdAsync(Guid id);
    }
}