using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using CostManager.CategoryService.Abstracts.Interfaces.Business;
using CostManager.CategoryService.Abstracts.Models.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CostManager.CategoryService.API.StorageQueue;

public class UpdateCategoryByQueue
{
    private readonly ILogger<UpdateCategoryByQueue> _logger;
    private readonly ICategoryService _categoryService;

    public UpdateCategoryByQueue(ILogger<UpdateCategoryByQueue> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function(nameof(UpdateCategoryByQueue))]
    [QueueOutput("update-category-result", Connection = "AzureWebJobsStorage")]
    public async Task<string> Run([QueueTrigger("update-category", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        
        try
        {
            var updateCategory = JsonSerializer.Deserialize<CategoryViewModel>(message.MessageText);
            var guid = await _categoryService.UpdateCategory(updateCategory);
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Success" });
            return outputMessage;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Failed to update category: {message.MessageId}");
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Failed" });
            return outputMessage;
        }
    }
}