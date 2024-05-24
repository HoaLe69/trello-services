using AutoMapper;
using trello_services.Entities;
using trello_services.Models.Response;

namespace trello_services.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() { 
            CreateMap<User , UserResponseVM>();
            CreateMap<Card , CardResponseVM>();
        }
    }
}
