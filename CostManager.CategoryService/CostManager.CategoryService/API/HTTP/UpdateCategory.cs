using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Models.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

namespace CostManager.CategoryService.API.HTTP;

public class UpdateCategory
{
    private readonly ILogger<UpdateCategory> _logger;
    private readonly ICategoryService _categoryService;

    public UpdateCategory(ILogger<UpdateCategory> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("UpdateCategory")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "put", Route = "category/update")] HttpRequest req,
        [FromBody] CategoryViewModel category)
    {
        _logger.LogInformation("Start processing Update category trigger function request.");
        var result = await _categoryService.UpdateCategory(category);
        _logger.LogInformation("Stop processing Update category trigger function request.");
        return new OkObjectResult(result);
    }
}