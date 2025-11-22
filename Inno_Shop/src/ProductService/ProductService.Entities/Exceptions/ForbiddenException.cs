namespace ProductService.Entities.Exceptions;

public class ForbiddenException : BadRequestException
{
    public ForbiddenException(string message) : base(message)
    {
    }
}