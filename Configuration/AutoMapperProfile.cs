using AutoMapper;
using trello_services.Entities;
using trello_services.Models.Request;
using trello_services.Models.Response;

namespace trello_services.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() { 
            CreateMap<User , UserResponseVM>();
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.displayName , opt => opt.MapFrom(src => src.displayName))
                .ForMember(dest => dest.avatar_path , opt => opt.MapFrom(src => src.avatar_path));
        }
    }
}
