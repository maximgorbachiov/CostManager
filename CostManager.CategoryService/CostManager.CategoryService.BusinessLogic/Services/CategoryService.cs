using CostManager.CategoryService.Abstracts.Extensions;
using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Interfaces.Data;
using CostManager.CategoryService.Abstracts.Models.Business;

namespace CostManager.CategoryService.BusinessLogic.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<string> AddCategory(NewCategoryViewModel newCategoryVm)
    {
        var category = newCategoryVm.ToCategory();
        var categoryId = await _categoryRepository.AddCategory(category);
        return categoryId;
    }

    public async Task<bool> UpdateCategory(CategoryViewModel categoryVm)
    {
        var category = categoryVm.ToCategory();
        bool success = await _categoryRepository.UpdateCategory(category);
        return success;
    }

    public async Task<CategoryViewModel> GetCategoryById(string userId, string categoryId)
    {
        var category = await _categoryRepository.GetCategoryById(userId, categoryId);
        var result = category?.FromCategory();
        return result;
    }

    public async Task<CategoryViewModel> GetCategoryByName(string userId, string categoryName)
    {
        var category = await _categoryRepository.GetCategoryByName(userId, categoryName);
        var result = category?.FromCategory();
        return result;
    }

    public async Task<List<CategoryViewModel>> GetCategories(string userId)
    {
        var categories = await _categoryRepository.GetCategories(userId);
        var result = categories?.Select(c => c.FromCategory()).ToList();
        return result;
    }

    public async Task<bool> RemoveCategory(string userId, string categoryId)
    {
        var isRemoved = await _categoryRepository.RemoveCategory(userId, categoryId);
        return isRemoved;
    }

    public async Task<bool> RemoveCategories(string userId)
    {
        var isRemoved = await _categoryRepository.RemoveCategories(userId);
        return isRemoved;
    }
}