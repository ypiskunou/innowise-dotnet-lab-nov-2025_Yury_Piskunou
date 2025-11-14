namespace UserService.Entities.Exceptions;

public sealed class InvalidPasswordException : BadRequestException
{
    public InvalidPasswordException()
        : base("Invalid email or password.")
    {
    }
}