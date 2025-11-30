using Entities.Exceptions;

namespace UserService.Entities.Exceptions;

public sealed class UserNotFoundException: NotFoundException
{
    public UserNotFoundException(Guid id) : 
        base($"User with id {id} not found")
    {
    }
}