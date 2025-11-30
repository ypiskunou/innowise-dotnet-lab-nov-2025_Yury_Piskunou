
namespace ProductService.Contracts;

public interface IRepositoryManager
{
    IProductRepository Product { get; }
    ICategoryRepository Category { get; }
    
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}