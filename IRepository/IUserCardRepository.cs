using trello_services.Entities;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IUserCardRepository
    {
        Task<UserCard> AddUserToCardAsync(Guid userId , Int64 cardId);
        Task RemoveUserFromCardAsync(Guid userId, Int64 cardId);
        Task<IList<UserResponseVM>> GetListUserInCardAsync(Guid userId);
    }
}
