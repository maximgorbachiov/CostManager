using Abstraction.Repositories;
using AutoMapper;
using Business.Data;
using Business.Services.Abstraction;
using Data.Models;

namespace BusinessLogic.CategoryService
{
    public class CategoryBusinessService : ICategoryBusinessService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryBusinessService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public CategoryModel GetCategory(Guid id)
        {
            var category = _categoryRepository.GetUntrackedEntity(id);

            var categoryModel = _mapper.Map<CategoryModel>(category);

            return categoryModel;
        }

        public IEnumerable<CategoryModel> GetCategories()
        {
            var categories = _categoryRepository.GetUntrackedEntities();

            var categoriesModels = categories.Select(c => _mapper.Map<CategoryModel>(c)).ToList();

            return categoriesModels;
        }

        public Guid CreateCategory(CategoryModel categoryModel)
        {
            var category = _mapper.Map<Category>(categoryModel);

            Guid categoryId = _categoryRepository.CreateUntrackedEntity(category);

            return categoryId;
        }
    }
}