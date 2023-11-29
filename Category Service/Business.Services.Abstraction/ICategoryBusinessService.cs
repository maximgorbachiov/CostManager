using Business.Data;

namespace Business.Services.Abstraction
{
    public interface ICategoryBusinessService
    {
        CategoryModel GetCategory(Guid id);
        IEnumerable<CategoryModel> GetCategories();
        Guid CreateCategory(CategoryModel categoryModel);
    }
}