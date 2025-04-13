using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.CategoryService.API.HTTP;

public class RemoveCategories
{
    private readonly ILogger<RemoveCategories> _logger;
    private readonly ICategoryService _categoryService;

    public RemoveCategories(ILogger<RemoveCategories> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("RemoveCategories")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "category/removeAllByUser/{userId:guid}")] HttpRequest req,
        Guid userId)
    {
        _logger.LogInformation("Start processing Remove categories trigger function request.");
        var result = await _categoryService.RemoveCategories(userId);
        _logger.LogInformation("Stop processing Remove categories trigger function request.");
        return new OkObjectResult(result);
    }
}