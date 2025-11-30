using ProductService.Entities.Models;

namespace ProductService.Repository.Extensions;

public static class RepositoryProductExtensions
{
    public static IQueryable<Product> FilterProducts(this IQueryable<Product> products, 
        decimal minPrice, decimal maxPrice, Guid? categoryId)
    {
        products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
        
        if (categoryId.HasValue)
        {
            products = products.Where(p => p.CategoryId == categoryId.Value);
        }

        return products;
    }
    
    public static IQueryable<Product> Search(this IQueryable<Product> products, string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return products;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return products.Where(p => p.Name.ToLower().Contains(lowerCaseTerm));
    }
    
    public static IQueryable<Product> Sort(this IQueryable<Product> products, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return products.OrderBy(e => e.Name);
        
        var orderQuery = orderByQueryString.Trim().ToLower();

        return orderQuery switch
        {
            "price" => products.OrderBy(p => p.Price),
            "price desc" => products.OrderByDescending(p => p.Price),
            "name desc" => products.OrderByDescending(p => p.Name),
            _ => products.OrderBy(p => p.Name)
        };
    }
}