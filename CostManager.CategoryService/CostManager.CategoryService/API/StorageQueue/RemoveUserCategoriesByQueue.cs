using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using CostManager.CategoryService.Abstracts.Interfaces.Business;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CostManager.CategoryService.API.StorageQueue;

public class RemoveUserCategoriesByQueue
{
    private readonly ILogger<RemoveUserCategoriesByQueue> _logger;
    private readonly ICategoryService _categoryService;

    public RemoveUserCategoriesByQueue(ILogger<RemoveUserCategoriesByQueue> logger, ICategoryService categoryService)
    {
        _logger = logger;
        _categoryService = categoryService;
    }

    [Function(nameof(RemoveUserCategoriesByQueue))]
    [QueueOutput("remove-user-categories-result", Connection = "AzureWebJobsStorage")]
    public async Task<string> Run([QueueTrigger("remove-user-categories", Connection = "AzureWebJobsStorages")] QueueMessage message)
    {
        _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        
        try
        {
            var userId = JsonSerializer.Deserialize<Guid>(message.MessageText);
            var result = await _categoryService.RemoveCategories(userId);
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Success" });
            return outputMessage;
        }
        catch (Exception e)
        {
            _logger.LogInformation($"Failed to remove user's categories: {message.MessageId}");
            var outputMessage = JsonSerializer.Serialize(new { messageId = message.MessageId, status = "Failed" });
            return outputMessage;
        }
    }
}