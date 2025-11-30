namespace UserService.Entities.Exceptions;

public sealed class AccountNotVerifiedException : BadRequestException
{
    public AccountNotVerifiedException()
        : base("Account is not verified. Please check your email.")
    {
    }
}