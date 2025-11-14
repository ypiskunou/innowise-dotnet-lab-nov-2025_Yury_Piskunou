using Entities.Exceptions;

namespace ProductService.Entities.Exceptions;

public sealed class CategoryNotFoundException: NotFoundException
{
    public CategoryNotFoundException(Guid id) :
        base($"Category with id {id} not found")
    {
    }
}