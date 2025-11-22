using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Entities.Models;

namespace ProductService.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Product?>> GetAllProductsAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(a => a.Name)
        .ToListAsync();

    public async Task<IEnumerable<Product?>> SearchProductsByNameAsync(string name, bool trackChanges) =>
        await FindByCondition(a => a.Name.ToLower().Contains(name.ToLower()), trackChanges)
            .OrderBy(a => a.Name)
            .ToListAsync();

    public IQueryable<Product?> GetProductsWithCategories(bool trackChanges) => 
        FindAll(trackChanges)
            .OrderBy(p => p.Name)
            .Include(p => p.Category);

    public async Task<IEnumerable<T>> GetProductsAsAsync<T>(IQueryable<Product?> Products,
        Expression<Func<Product, T>> selector)
    {
        return await Products.Select(selector).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id, bool trackChanges,
        CancellationToken cancellationToken) =>
        await FindByCondition(a => a.Id == id, trackChanges)
            .FirstOrDefaultAsync(cancellationToken);

    public void CreateProduct(Product Product) => Create(Product);

    public void DeleteProduct(Product Product) => Delete(Product);
}