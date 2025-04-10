using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Abstracts.Interfaces.Data;

public interface ICategoryRepository
{
    Task<string> AddCategory(Category category);
    Task<bool> UpdateCategory(Category category);
    Task<Category?> GetCategoryById(string userId, string categoryId);
    Task<Category?> GetCategoryByName(string userId, string categoryName);
    Task<List<Category>?> GetCategories(string userId);
    Task<CategoryWithChildren?> GetCategoryWithChildren(string userId, string categoryId);
    Task<bool> RemoveCategory(string userId, string categoryId);
    Task<bool> RemoveCategories(string userId);
}