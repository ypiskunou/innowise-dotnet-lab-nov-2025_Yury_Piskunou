using System.Linq.Expressions;
using ProductService.Entities.Models;
using ProductService.Shared.RequestFeatures;

namespace ProductService.Contracts;

public interface IProductRepository
{
    Task<PagedList<Product?>> GetAllProductsAsync(ProductParameters productParameters, 
        bool trackChanges);
    Task<Product?> GetProductByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken = default);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);

    Task<bool> HasProductsInCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<Product>> GetProductsByUserIdAsync(Guid userId,  bool trackChanges, bool asOwner,
        CancellationToken cancellationToken = default);
    
    IQueryable<Product?> GetProductsQuery(bool trackChanges);
    Task<IEnumerable<T>> GetProductsAsAsync<T>(IQueryable<Product?> products, Expression<Func<Product, T>> selector);
    
}