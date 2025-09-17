using AutoMapper;
using Codefolio.API.Dto;

namespace Codefolio.API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, AuthDto>()
            .ForMember(dest => dest.HashedEmail, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.HashedPassword, opt => opt.MapFrom(src => src.Password));

        CreateMap<AuthDto, UserDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.HashedEmail))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.HashedPassword));
    }
}