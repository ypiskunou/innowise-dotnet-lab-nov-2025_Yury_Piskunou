using MediatR;
using UserService.Shared.DataTransferObjects;

namespace UserService.Application.Features.Users.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;