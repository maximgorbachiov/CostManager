namespace Web.Data.Models.Category
{
    public class CategoryViewModelOnCreate
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
