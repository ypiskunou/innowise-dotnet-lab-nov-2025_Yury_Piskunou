using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Entities.Models;
using ProductService.Repository.Extensions;
using ProductService.Shared.RequestFeatures;

namespace ProductService.Repository;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Product?>> GetAllProductsAsync(ProductParameters productParameters, 
        bool trackChanges)
    {
        var products = FindAll(trackChanges); 
        
        products = products
            .FilterProducts(productParameters.MinPrice, productParameters.MaxPrice, productParameters.CategoryId)
            .Search(productParameters.SearchTerm)
            .Sort(productParameters.OrderBy);
        
        return await products.ToPagedListAsync(productParameters.PageNumber, productParameters.PageSize);
    }

    public async Task<Product?> GetProductByIdAsync(Guid id, bool trackChanges,
        CancellationToken cancellationToken) =>
        await FindByCondition(a => a.Id == id, trackChanges)
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(cancellationToken);

    public void CreateProduct(Product product) => Create(product);

    public void DeleteProduct(Product product) => Delete(product);

    public async Task<bool> HasProductsInCategoryAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        return await FindByCondition(p => p.CategoryId == categoryId, trackChanges: false)
            .IgnoreQueryFilters()
            .AnyAsync(cancellationToken);
    }

    public async Task<IEnumerable<Product>> GetProductsByUserIdAsync(
        Guid userId,
        bool trackChanges,
        bool asOwner,
        CancellationToken token)
    {
        var query = FindByCondition(p => p.UserId == userId, trackChanges);

        if (asOwner)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ToListAsync(token);
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(
        Guid categoryId,
        bool trackChanges,
        bool includeHidden,
        CancellationToken cancellationToken)
    {
        var query = FindByCondition(p => p.CategoryId == categoryId, trackChanges);

        if (includeHidden)
        {
            query = query.IgnoreQueryFilters();
        }

        return await query.ToListAsync(cancellationToken);
    }

    public IQueryable<Product> GetProductsQuery(bool trackChanges) =>
        FindAll(trackChanges);

    public async Task<IEnumerable<T>> GetProductsAsAsync<T>(IQueryable<Product?> Products,
        Expression<Func<Product, T>> selector)
    {
        return await Products.Select(selector).ToListAsync();
    }
}