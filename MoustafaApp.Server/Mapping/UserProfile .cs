using MoustafaApp.Server.Dtos.UserDtos;

namespace MoustafaApp.Server.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.FullName,opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UpdateUserDto, ApplicationUser>();
        }
    }
}
