using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.CategoryService.API.HTTP;

public class GetById
{
    private readonly ILogger<GetById> _logger;
    private readonly ICategoryService _categoryService;

    public GetById(ILogger<GetById> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("GetById")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "category/get/{userId:alpha}/{categoryId:alpha}")] HttpRequest req,
        string userId,
        string categoryId)
    {
        _logger.LogInformation("Start processing Get category by Id trigger function request.");
        var result = await _categoryService.GetCategoryById(userId, categoryId);
        _logger.LogInformation("Stop processing Get category by Id trigger function request.");
        return new OkObjectResult(result);
    }
}