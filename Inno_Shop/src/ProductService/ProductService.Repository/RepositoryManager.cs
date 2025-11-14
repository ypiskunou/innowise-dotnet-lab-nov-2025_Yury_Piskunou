
using ProductService.Contracts;

namespace ProductService.Repository;

public class RepositoryManager: IRepositoryManager
{
    private readonly RepositoryContext _context;
    
    private readonly Lazy<IProductRepository> _product;
    private readonly Lazy<ICategoryRepository> _book;

    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _product = new Lazy<IProductRepository>(() => new ProductRepository(context));
        _book = new Lazy<ICategoryRepository>(() => new CategoryRepository(context));
    }
    
    public IProductRepository Product => _product.Value;
    public ICategoryRepository Category => _book.Value;
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => 
        await _context.SaveChangesAsync(cancellationToken);
}