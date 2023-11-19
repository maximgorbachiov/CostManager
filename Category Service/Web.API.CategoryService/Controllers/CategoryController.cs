using Business.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Web.API.CategoryService.Models.CategoryViewModels;
using Web.Data.Models.Category;

namespace Web.API.CategoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryBusinessService _categoryBusinessService;

        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryBusinessService categoryBusinessService,
            ILogger<CategoryController> logger)
        {
            _categoryBusinessService = categoryBusinessService;
            _logger = logger;
        }

        [HttpGet(Name = "GetCategories")]
        public IEnumerable<CategoryViewModelOnRead> GetCategories()
        {
            var categories = _categoryBusinessService.GetCategories();

            // some mapping

            return Enumerable.Empty<CategoryViewModelOnRead>();
        }
    }
}
