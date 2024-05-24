using trello_services.Models.Response;

namespace trello_services.IRepository
{
    public interface IJwtServices
    {
        string GenerateToken(UserResponseVM user);
    }
}
