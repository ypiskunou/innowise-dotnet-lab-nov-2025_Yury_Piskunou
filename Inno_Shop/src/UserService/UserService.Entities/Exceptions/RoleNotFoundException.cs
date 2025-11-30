using Entities.Exceptions;

namespace UserService.Entities.Exceptions;

public sealed class RoleNotFoundException: NotFoundException
{
    public RoleNotFoundException(Guid id) :
        base($"Role with id {id} not found")
    {
    }
    
    public RoleNotFoundException(string name) :
        base($"Role with name {name} not found")
    {
    }
}