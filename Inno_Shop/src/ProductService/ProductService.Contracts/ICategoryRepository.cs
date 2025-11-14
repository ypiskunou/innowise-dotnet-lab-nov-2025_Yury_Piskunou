using ProductService.Entities.Models;

namespace ProductService.Contracts;

public interface ICategoryRepository
{
    Task<IEnumerable<Category?>> GetAllCategoriesAsync(bool trackChanges);
    Task<Category?> GetCategoryByIdAsync(Guid id, bool trackChanges);
    void CreateCategory(Category category);
    void DeleteCategory(Category category);
}