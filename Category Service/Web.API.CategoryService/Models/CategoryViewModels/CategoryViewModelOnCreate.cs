namespace Web.API.CategoryService.Models.CategoryViewModels
{
    public class CategoryViewModelOnCreate
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
