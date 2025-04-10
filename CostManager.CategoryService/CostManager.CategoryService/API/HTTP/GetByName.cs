using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.CategoryService.API.HTTP;

public class GetByName
{
    private readonly ILogger<GetById> _logger;
    private readonly ICategoryService _categoryService;

    public GetByName(ILogger<GetById> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("GetByName")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "category/get/{userId:alpha}/{categoryName:alpha}")] HttpRequest req,
        string userId,
        string categoryName)
    {
        _logger.LogInformation("Start processing Get category by Name trigger function request.");
        var result = await _categoryService.GetCategoryByName(userId, categoryName);
        _logger.LogInformation("Stop processing Get category by Name trigger function request.");
        return new OkObjectResult(result);
    }
}