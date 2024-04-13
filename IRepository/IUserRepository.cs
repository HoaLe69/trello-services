using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IUserRepository
    {
        Task<UserResponseVM> UpdateUserAsync(UserRequestModel request , Guid id);
        Task<UserResponseVM> GetByIdAsync(Guid id);
        Task<UserResponseVM> GetUserByEmailAsync(string email);
        Task<List<UserResponseVM>> GetByListIdAsync(Guid[] ids);
        Task<UserResponseVM> CreateNewUserAsync(UserRequestModel request);
    }
}
