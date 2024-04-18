using trello_services.Entities;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IUserBoardRepository
    {
        Task<UserBoard> AddUserToBoardAsync(UserBoard user);
        Task<UserBoard> UpdateRoleOfUserAsync(Guid userId, Guid boardId, Role? role);
        Task RemoveUserFromBoardAsync(Guid userId, Guid boardId);
        Task<IList<UserResponseVM>> GetListUserOfBoardAsync(Guid boardId);
    }
}
