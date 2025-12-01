using AutoMapper;
using UserService.Entities.Models;
using UserService.Shared.DataTransferObjects;

namespace UserService.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserForRegistrationDto, User>();
        
        CreateMap<UserForUpdateDto, User>();
        
        CreateMap<Role, RoleDto>();
        
        CreateMap<User, UserDto>()
            .ForCtorParam("Roles", opt => opt
                .MapFrom(src => src.Roles.Select(r => r.Name)));
    }
}