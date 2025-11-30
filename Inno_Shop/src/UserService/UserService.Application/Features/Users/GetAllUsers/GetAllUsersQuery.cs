using MediatR;
using UserService.Shared.DataTransferObjects;
using UserService.Shared.RequestFeatures;

namespace UserService.Application.Features.Users.GetAllUsers;

public record GetAllUsersQuery(UserParameters UserParameters) 
    : IRequest<(IEnumerable<UserDto> users, MetaData metaData)>;
