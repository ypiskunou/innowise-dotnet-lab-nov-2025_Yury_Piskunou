using MediatR;
using Shared.DataTransferObjects;

namespace UserService.Service.Features.Authentication.AuthenticateUser;

public record AuthenticateUserQuery(UserForAuthenticationDto UserForAuth) : IRequest<TokenDto>;