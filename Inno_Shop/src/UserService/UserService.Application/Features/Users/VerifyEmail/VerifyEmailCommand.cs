using MediatR;

namespace UserService.Application.Features.Users.VerifyEmail;

public record VerifyEmailCommand(string Email, string Token) : IRequest<bool>;