namespace CostManager.CategoryService.Abstracts.Models.Data;

public class CategoryWithChildren : Category
{
    public List<CategoryWithChildren> ChildrenCategories { get; set; }
}