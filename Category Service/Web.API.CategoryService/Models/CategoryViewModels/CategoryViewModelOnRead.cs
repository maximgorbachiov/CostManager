namespace Web.API.CategoryService.Models.CategoryViewModels
{
    public class CategoryViewModelOnRead
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
