using Microsoft.EntityFrameworkCore;
using ProductService.Contracts;
using ProductService.Entities.Models;

namespace ProductService.Repository;

public class CategoryRepository: RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<Category?>> GetAllCategoriesAsync(bool trackChanges) => await FindAll(trackChanges)
        .OrderBy(c => c.Name)
        .ToListAsync();

    public async Task<Category?> GetCategoryByIdAsync(Guid id, bool trackChanges) =>
        await FindByCondition(b => b.Id == id, trackChanges).FirstOrDefaultAsync();

    public void CreateCategory(Category Category) => Create(Category);

    public void DeleteCategory(Category Category) => Delete(Category);
}