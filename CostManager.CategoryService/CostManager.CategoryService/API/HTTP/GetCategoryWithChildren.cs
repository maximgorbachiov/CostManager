using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.CategoryService.API.HTTP;

public class GetCategoryWithChildren
{
    private readonly ILogger<GetCategoryWithChildren> _logger;
    private readonly ICategoryService _categoryService;

    public GetCategoryWithChildren(ILogger<GetCategoryWithChildren> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("GetCategoryWithChildren")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "category/getWithChildren/{userId:guid}/{categoryId:guid}")] HttpRequest req,
        Guid userId,
        Guid categoryId)
    {
        _logger.LogInformation("Start processing Get category with children trigger function request.");
        var result = await _categoryService.GetCategoryWithChildren(userId, categoryId);
        _logger.LogInformation("Stop processing Get category with children trigger function request.");
        return new OkObjectResult(result);
    }
}