using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IAuth
    {
        Task<UserResponseVM> AuthenticatedAsync(UserLoginModel login);
        Task<bool> RegisterAsync(UserLoginModel register);
        Task<bool> VerifyEmailAsync(UserLoginModel register);
    }
}
