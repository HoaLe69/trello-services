using trello_services.Entities;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IUserBoardRepository
    {
        Task<UserResponseVM> AddUserToBoardAsync(UserBoardRequestModel user);
        Task<IList<UserBoard>> GetBoardByUserId(Guid userId);
        Task<UserBoard> UpdateRoleOfUserAsync(Guid userId, Guid boardId, Role? role);
        Task RemoveUserFromBoardAsync(Guid userId, Guid boardId);
        Task<IList<UserBoard>> GetListUserOfBoardAsync(Guid boardId);
        Task AddBoardToFavouriteListUserAsync(Guid userId, Guid boardId , bool star);
    }
}
