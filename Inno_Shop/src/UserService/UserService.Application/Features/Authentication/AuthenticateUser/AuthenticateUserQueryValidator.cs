using FluentValidation;

namespace UserService.Application.Features.Authentication.AuthenticateUser;

public class AuthenticateUserQueryValidator : AbstractValidator<AuthenticateUserQuery>
{
    public AuthenticateUserQueryValidator()
    {
        RuleFor(x => x.UserForAuth).NotNull();
        
        RuleFor(x => x.UserForAuth.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
            
        RuleFor(x => x.UserForAuth.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}