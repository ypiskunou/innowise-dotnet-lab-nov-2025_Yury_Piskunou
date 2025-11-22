using MediatR;
using Shared.DataTransferObjects;

namespace UserService.Application.Features.Users.RegisterUser;

public record RegisterUserCommand(UserForRegistrationDto UserForRegistration) : IRequest;