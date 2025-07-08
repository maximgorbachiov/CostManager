using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Queues.Models;
using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CostManager.CategoryService.API.StorageQueue;

public class RemoveCategoryByQueue
{
    private readonly ILogger<RemoveCategoryByQueue> _logger;
    private readonly ICategoryService _categoryService;

    public RemoveCategoryByQueue(ILogger<RemoveCategoryByQueue> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function(nameof(RemoveCategoryByQueue))]
    [QueueOutput("remove-category-result", Connection = "AzureWebJobsStorage")]
    public async Task<string> Run([QueueTrigger("remove-category", Connection = "AzureWebJobsStorage")] QueueMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        
        try
        {
            var removeCategory = JsonSerializer.Deserialize<RemoveCategoryRequest>(message.MessageText);
            var result = await _categoryService.RemoveCategory(removeCategory.UserId, removeCategory.CategoryId);
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Success" });
            return outputMessage;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Failed to remove category: {message.MessageId}");
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Failed" });
            return outputMessage;
        }
    }

    private class RemoveCategoryRequest
    {
        [JsonPropertyName("cagetoryId")]
        public Guid CategoryId { get; set; }
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
    }
}