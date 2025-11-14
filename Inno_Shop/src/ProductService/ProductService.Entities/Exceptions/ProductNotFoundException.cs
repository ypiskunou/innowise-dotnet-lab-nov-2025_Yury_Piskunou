using Entities.Exceptions;

namespace ProductService.Entities.Exceptions;

public sealed class ProductNotFoundException: NotFoundException
{
    public ProductNotFoundException(Guid id) : 
        base($"Product with id {id} not found")
    {
    }
}