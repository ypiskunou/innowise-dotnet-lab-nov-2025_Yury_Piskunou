using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Entities.Models;
using ProductService.Shared.DataTransferObjects;

namespace ProductService.Repository;

public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Category?>> GetAllCategoriesAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync();
    
    public async Task<IEnumerable<Category>> GetAllCategoriesWithCountsAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Select(c => new Category
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ProductCount = c.Products.Count() 
            })
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(Guid id, bool trackChanges, CancellationToken token) =>
        await FindByCondition(b => b.Id == id, trackChanges).FirstOrDefaultAsync(token);

    public void CreateCategory(Category Category) => Create(Category);
    
    public void UpdateCategory(Category category) => Update(category);

    public void DeleteCategory(Category Category) => Delete(Category);
}