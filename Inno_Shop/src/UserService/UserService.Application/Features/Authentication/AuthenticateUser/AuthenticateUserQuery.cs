using MediatR;
using Shared.DataTransferObjects;

namespace UserService.Application.Features.Authentication.AuthenticateUser;

public record AuthenticateUserQuery(UserForAuthenticationDto UserForAuth) : IRequest<TokenDto>;