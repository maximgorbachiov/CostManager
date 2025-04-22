using CostManager.CategoryService.Abstracts.Models.Data;

namespace CostManager.CategoryService.Abstracts.Models.Business;

public class CategoryViewModel
{
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}