using MediatR;
using Shared.DataTransferObjects;

namespace UserService.Application.Features.Users.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IEnumerable<UserDto>>;
