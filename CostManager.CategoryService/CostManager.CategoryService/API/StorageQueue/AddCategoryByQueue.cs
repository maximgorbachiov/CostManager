using System.Text.Json;
using Azure.Storage.Queues.Models;
using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Models.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CostManager.CategoryService.API.StorageQueue;

public class AddCategoryByQueue
{
    private readonly ILogger<AddCategoryByQueue> _logger;
    private readonly ICategoryService _categoryService;

    public AddCategoryByQueue(ILogger<AddCategoryByQueue> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function(nameof(AddCategoryByQueue))]
    [QueueOutput("add-category-result", Connection = "AzureWebJobsStorage")]
    public async Task<string> Run([QueueTrigger("add-category", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function started processing: {message.MessageId}");

        try
        {
            var newCategory = JsonSerializer.Deserialize<NewCategoryViewModel>(message.MessageText);
            var guid = await _categoryService.AddCategory(newCategory);
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Success", categoryId = guid });
            return outputMessage;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Failed to create category: {message.MessageId}");
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Failed" });
            return outputMessage;
        }
    }
}