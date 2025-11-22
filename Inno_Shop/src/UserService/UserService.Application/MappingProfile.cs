using AutoMapper;
using Shared.DataTransferObjects;
using UserService.Entities.Models;

namespace UserService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegistrationDto, User>();
        
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Roles,
                opt => opt.MapFrom(src => src.Roles.Select(r => r.Name)));

        
        CreateMap<Role, RoleDto>();
    }
}