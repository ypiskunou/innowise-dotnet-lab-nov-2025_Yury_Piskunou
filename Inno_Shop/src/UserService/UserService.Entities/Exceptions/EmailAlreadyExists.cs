namespace UserService.Entities.Exceptions;

public sealed class EmailAlreadyExistsException : BadRequestException
{
    public EmailAlreadyExistsException(string email)
        : base($"The email '{email}' is already registered.")
    {
    }
}