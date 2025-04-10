using CostManager.CategoryService.Abstracts.Models.Business;

namespace CostManager.CategoryService.Abstracts.Interfaces.Business;

public interface ICategoryService
{
    Task<string> AddCategory(NewCategoryViewModel newCategoryVm);
    Task<bool> UpdateCategory(CategoryViewModel categoryVm);
    Task<CategoryViewModel> GetCategoryById(string userId, string categoryId);
    Task<CategoryViewModel> GetCategoryByName(string userId, string categoryName);
    Task<List<CategoryViewModel>> GetCategories(string userId);
    Task<bool> RemoveCategory(string userId, string categoryId);
    Task<bool> RemoveCategories(string userId);
}