using AutoMapper;
using Business.Data;
using Business.Services.Abstraction;
using Web.Data.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusinessService _categoryBusinessService;
        private IMapper _mapper;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryBusinessService categoryBusinessService,
            IMapper mapper,
            ILogger<CategoryController> logger)
        {
            _categoryBusinessService = categoryBusinessService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet(Name = "GetCategories")]
        public IEnumerable<CategoryViewModelOnRead> GetCategories()
        {
            var categories = _categoryBusinessService.GetCategories();

            var categoriesViewModels = categories.Select(c => _mapper.Map<CategoryViewModelOnRead>(c)).ToList();

            return categoriesViewModels;
        }

        [HttpPost(Name = "CreateCategory")]
        public Guid CreateCategory(CategoryViewModelOnCreate categoryViewModelOnCreate)
        {
            var categoryModel = _mapper.Map<CategoryModel>(categoryViewModelOnCreate);

            Guid categoryId = _categoryBusinessService.CreateCategory(categoryModel);

            return categoryId;
        }
    }
}
