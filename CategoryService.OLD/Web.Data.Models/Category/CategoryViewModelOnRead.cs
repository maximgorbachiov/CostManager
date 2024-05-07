namespace Web.Data.Models.Category
{
    public class CategoryViewModelOnRead
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Guid? ParentCategoryId { get; set; }
        public Guid UserId { get; set; }
    }
}
