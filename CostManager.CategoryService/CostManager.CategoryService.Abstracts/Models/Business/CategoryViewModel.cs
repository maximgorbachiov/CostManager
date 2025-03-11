using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Abstracts.Models.Business;

public class CategoryViewModel
{
    public string CategoryId { get; set; }
    public string UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ParentCategoryId { get; set; }
}