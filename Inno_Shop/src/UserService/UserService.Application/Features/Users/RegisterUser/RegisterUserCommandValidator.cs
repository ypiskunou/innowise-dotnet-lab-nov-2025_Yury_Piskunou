using FluentValidation;

namespace UserService.Service.Features.Users.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.UserForRegistration.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.UserForRegistration.Password).NotEmpty().MinimumLength(8);
        RuleFor(x => x.UserForRegistration.ConfirmPassword)
            .Equal(x => x.UserForRegistration.Password).WithMessage("Пароли не совпадают.");
    }
}