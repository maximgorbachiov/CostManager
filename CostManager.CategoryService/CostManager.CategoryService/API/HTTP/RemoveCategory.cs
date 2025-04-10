using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.CategoryService.API.HTTP;

public class RemoveCategory
{
    private readonly ILogger<RemoveCategory> _logger;
    private readonly ICategoryService _categoryService;

    public RemoveCategory(ILogger<RemoveCategory> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("RemoveCategory")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "category/remove/{userId:alpha}/{categoryId:alpha}")] HttpRequest req,
        string userId,
        string categoryId)
    {
        _logger.LogInformation("Start processing Remove category trigger function request.");
        var result = await _categoryService.RemoveCategory(userId, categoryId);
        _logger.LogInformation("Stop processing Remove category trigger function request.");
        return new OkObjectResult(result);
    }
}