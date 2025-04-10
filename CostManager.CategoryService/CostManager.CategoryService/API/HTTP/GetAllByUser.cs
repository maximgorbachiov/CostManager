using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.CategoryService.API.HTTP;

public class GetAllByUser
{
    private readonly ILogger<GetAllByUser> _logger;
    private readonly ICategoryService _categoryService;

    public GetAllByUser(ILogger<GetAllByUser> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function("GetAllByUser")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "category/get/all/{userId:alpha}")] HttpRequest req,
        string userId)
    {
        _logger.LogInformation("Start processing Get categories by User trigger function request.");
        var result = await _categoryService.GetCategories(userId);
        _logger.LogInformation("Stop processing Get categories by User trigger function request.");
        return new OkObjectResult(result);
    }
}