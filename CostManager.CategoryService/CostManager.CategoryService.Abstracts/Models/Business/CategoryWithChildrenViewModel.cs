namespace CostManager.CategoryService.Abstracts.Models.Business;

public class CategoryWithChildrenViewModel : CategoryViewModel
{
    public List<CategoryWithChildrenViewModel> ChildrenCategories { get; set; }
}