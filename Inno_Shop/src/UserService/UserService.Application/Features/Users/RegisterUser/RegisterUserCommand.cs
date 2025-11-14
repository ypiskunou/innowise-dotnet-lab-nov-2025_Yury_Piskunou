using MediatR;
using Shared.DataTransferObjects;

namespace UserService.Service.Features.Users.RegisterUser;

public record RegisterUserCommand(UserForRegistrationDto UserForRegistration) : IRequest;