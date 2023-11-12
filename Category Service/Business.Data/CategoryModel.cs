namespace Business.Data
{
    public class CategoryModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}