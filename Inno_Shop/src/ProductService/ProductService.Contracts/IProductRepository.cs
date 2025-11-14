using System.Linq.Expressions;
using ProductService.Entities.Models;

namespace ProductService.Contracts;

public interface IProductRepository
{
    Task<IEnumerable<Product?>> GetAllProductsAsync(bool trackChanges);
    Task<Product?> GetProductByIdAsync(Guid id, bool trackChanges);
    void CreateProduct(Product product);
    void DeleteProduct(Product product);
    
    Task<IEnumerable<Product?>> SearchProductsByNameAsync(string name, bool trackChanges);
    
    IQueryable<Product?> GetProductsWithCategories(bool trackChanges);
    Task<IEnumerable<T>> GetProductsAsAsync<T>(IQueryable<Product?> products, Expression<Func<Product, T>> selector);
    
}