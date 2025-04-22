using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Repositories.Extensions;

public static class CategoryExtensions
{
    public static Category Update(this Category category, Category categoryToUpdate)
    {
        category.ParentCategoryId = categoryToUpdate.ParentCategoryId;
        category.Description = categoryToUpdate.Description;
        category.Title = categoryToUpdate.Title;
        
        return category;
    }
    
    public static CategoryWithChildren ToCategoryWithChildren(this Category category)
    {
        var categoryWithChildren = new CategoryWithChildren
        {
            ParentCategoryId = category.ParentCategoryId,
            Description = category.Description,
            Title = category.Title,
            CategoryId = category.CategoryId,
            ChildrenCategories = new List<CategoryWithChildren>()
        };
        
        return categoryWithChildren;
    }
}