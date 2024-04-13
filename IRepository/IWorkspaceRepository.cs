using trello_services.Entities;
using trello_services.Models.Request;

namespace trello_services.IRepository
{
    public interface IWorkspaceRepository
    {
        Task<IList<WorkSpace>> GetAllByUserIdAsync(Guid id);
        Task<WorkSpace> CreateWorkspaceAsync(WorkspaceRequestModel request);
        Task<WorkSpace> UpdateWorkspaceAsync(WorkspaceRequestModel request , Guid id);
        void DeleteWorkspaceAsync(WorkSpace workspace);
        Task<WorkSpace> GetWorkspaceByIdAsync(Guid id);
    }
}