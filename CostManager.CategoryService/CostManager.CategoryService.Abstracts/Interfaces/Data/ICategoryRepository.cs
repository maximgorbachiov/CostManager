using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Abstracts.Interfaces.Data;

public interface ICategoryRepository
{
    Task<Guid> AddCategory(Category category);
    Task<bool> UpdateCategory(Category category);
    Task<Category?> GetCategoryById(Guid userId, Guid categoryId);
    Task<Category?> GetCategoryByName(Guid userId, string categoryName);
    Task<List<Category>?> GetCategories(Guid userId);
    Task<CategoryWithChildren?> GetCategoryWithChildren(Guid userId, Guid categoryId);
    Task<bool> RemoveCategory(Guid userId, Guid categoryId);
    Task<bool> RemoveCategories(Guid userId);
}