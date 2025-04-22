namespace CostManager.CategoryService.Abstracts.Models.Business;

public class NewCategoryViewModel
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}