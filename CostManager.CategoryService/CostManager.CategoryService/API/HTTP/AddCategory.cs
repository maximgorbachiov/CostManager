using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Models.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace CostManager.CategoryService.API.HTTP;

public class AddCategory
{
    private readonly ILogger<AddCategory> _logger;
    private readonly ICategoryService _categoryService;

    public AddCategory(ILogger<AddCategory> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("AddCategory")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "category/add")] HttpRequest req, 
        [FromBody] NewCategoryViewModel newCategory)
    {
        _logger.LogInformation("Start processing Add category trigger function request.");
        var result = await _categoryService.AddCategory(newCategory);
        _logger.LogInformation("Stop processing Add category trigger function request.");
        return new OkObjectResult(result);
    }
}