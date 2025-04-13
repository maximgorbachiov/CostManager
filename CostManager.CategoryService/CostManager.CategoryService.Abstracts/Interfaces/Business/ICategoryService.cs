using CostManager.CategoryService.Abstracts.Models.Business;

namespace CostManager.CategoryService.Abstracts.Interfaces.Business;

public interface ICategoryService
{
    Task<Guid> AddCategory(NewCategoryViewModel newCategoryVm);
    Task<bool> UpdateCategory(CategoryViewModel categoryVm);
    Task<CategoryViewModel> GetCategoryById(Guid userId, Guid categoryId);
    Task<CategoryViewModel> GetCategoryByName(Guid userId, string categoryName);
    Task<List<CategoryViewModel>> GetCategories(Guid userId);
    Task<CategoryWithChildrenViewModel?> GetCategoryWithChildren(Guid userId, Guid categoryId);
    Task<bool> RemoveCategory(Guid userId, Guid categoryId);
    Task<bool> RemoveCategories(Guid userId);
}