using CostManager.CategoryService.Abstracts.Models.Business;
using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Abstracts.Extensions;

public static class DataExtensions
{
    public static Category ToCategory(this NewCategoryViewModel newCategoryViewModel)
    {
        var category = new Category
        {
            Description = newCategoryViewModel.Description,
            ParentCategoryId = newCategoryViewModel.ParentCategoryId,
            UserId = newCategoryViewModel.UserId,
            Title = newCategoryViewModel.Title,
        };
        
        return category;
    }
    
    public static Category ToCategory(this CategoryViewModel categoryViewModel)
    {
        var category = new Category
        {
            Description = categoryViewModel.Description,
            ParentCategoryId = categoryViewModel.ParentCategoryId,
            UserId = categoryViewModel.UserId,
            Title = categoryViewModel.Title,
            CategoryId = categoryViewModel.CategoryId
        };
        
        return category;
    }
    
    public static CategoryViewModel FromCategory(this Category category)
    {
        var categoryViewModel = new CategoryViewModel
        {
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            UserId = category.UserId,
            Title = category.Title,
            CategoryId = category.CategoryId
        };
        
        return categoryViewModel;
    }
    
    public static CategoryWithChildrenViewModel FromCategory(this CategoryWithChildren categoryWithChildren)
    {
        var categoryViewModel = new CategoryWithChildrenViewModel
        {
            Description = categoryWithChildren.Description,
            ParentCategoryId = categoryWithChildren.ParentCategoryId,
            UserId = categoryWithChildren.UserId,
            Title = categoryWithChildren.Title,
            CategoryId = categoryWithChildren.CategoryId,
            ChildrenCategories = categoryWithChildren.ChildrenCategories?.Select(x => x.FromCategory()).ToList()
        };
        
        return categoryViewModel;
    }
}