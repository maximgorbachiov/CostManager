using Abstraction.Repositories;
using Business.Data;
using Business.Services.Abstraction;

namespace BusinessLogic.CategoryService
{
    public class CategoryBusinessService : ICategoryBusinessService
    {
        private ICategoryRepository _categoryRepository;

        public CategoryBusinessService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public CategoryModel GetCategory(Guid id)
        {
            var category = _categoryRepository.GetUntrackedEntity(id);

            // Some business logic and mapping

            return new CategoryModel();
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            var category = _categoryRepository.GetUntrackedEntities();

            // Some business logic and mapping

            return Enumerable.Empty<CategoryModel>();
        }
    }
}