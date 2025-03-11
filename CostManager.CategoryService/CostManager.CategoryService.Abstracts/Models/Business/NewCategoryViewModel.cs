using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Abstracts.Models.Business;

public class NewCategoryViewModel
{
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ParentCategoryId { get; set; }

    public Category ToCategory()
    {
        var category = new Category
        {
            Description = Description,
            ParentCategoryId = ParentCategoryId,
            UserId = UserId,
            Title = Title
        };
        
        return category;
    }
}