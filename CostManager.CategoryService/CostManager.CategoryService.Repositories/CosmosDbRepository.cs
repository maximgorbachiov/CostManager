using System.Net;
using CostManager.CategoryService.Abstracts.Interfaces.Data;
using CostManager.CategoryService.Abstracts.Models.Data;
using CostManager.CategoryService.Repositories.Extensions;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;

namespace CostManager.CategoryService.Repositories;

public class CosmosDbRepository : ICategoryRepository
{
    private readonly CosmosClient _cosmosClient;
    private readonly ILogger<CosmosDbRepository> _logger;

    public CosmosDbRepository(CosmosClient cosmosClient, ILogger<CosmosDbRepository> logger)
    {
        _cosmosClient = cosmosClient;
        _logger = logger;
    }

    public async Task<Guid> AddCategory(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));
        if (category.UserId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {category.UserId}");
        if (string.IsNullOrEmpty(category.Title))
            throw new ArgumentException($"Title can't be null or empty: {category.Title}");

        var container = await GetTransactionsContainer();

        var newCategory = new Category()
        {
            // In future category id should come with category object from api gate and linked with transaction
            CategoryId = Guid.NewGuid(),
            Title = category.Title,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            UserId = category.UserId
        };

        Category createdItem = null;

        try
        {
            createdItem = await container.CreateItemAsync(
                item: newCategory,
                partitionKey: new PartitionKey(newCategory.UserId.ToString()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return createdItem?.CategoryId ?? Guid.Empty;
    }

    public async Task<bool> UpdateCategory(Category category)
    {
        if (category == null)
            throw new ArgumentNullException(nameof(category));
        if (category.UserId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {category.UserId}");
        if (category.CategoryId == Guid.Empty)
            throw new ArgumentException($"Category should not be null or empty: {category.CategoryId}");
        if (string.IsNullOrEmpty(category.Title))
            throw new ArgumentException($"Title can't be null or empty: {category.Title}");

        var container = await GetTransactionsContainer();

        Category updatedCategory = null;
        try
        {
            updatedCategory = await container.UpsertItemAsync(
                category,
                partitionKey: new PartitionKey(category.UserId.ToString())
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return updatedCategory != null;
    }

    public async Task<Category?> GetCategoryById(Guid userId, Guid categoryId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        if (categoryId == Guid.Empty)
            throw new ArgumentException($"Category should not be null or empty: {categoryId}");

        var container = await GetTransactionsContainer();

        Category? category = null;
        try
        {
            category = await container.ReadItemAsync<Category>(
                id: categoryId.ToString(),
                partitionKey: new PartitionKey(userId.ToString())
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return category;
    }

    public async Task<Category?> GetCategoryByName(Guid userId, string categoryName)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        if (string.IsNullOrEmpty(categoryName))
            throw new ArgumentException($"Category name should not be null or empty: {categoryName}");

        var container = await GetTransactionsContainer();

        Category? category = null;
        try
        {
            var iterator = container.GetItemLinqQueryable<Category>(requestOptions: new QueryRequestOptions
            {
                PartitionKey = new PartitionKey(userId.ToString()),
                MaxItemCount = 1
            })
            .Where(c => c.Title == categoryName)
            .ToFeedIterator();

            while (iterator.HasMoreResults)
            {
                foreach (var item in await iterator.ReadNextAsync())
                {
                    category = item;
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return category;
    }

    public async Task<List<Category>?> GetCategories(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {userId}");

        var container = await GetTransactionsContainer();

        var query = new QueryDefinition(
            query: "SELECT * FROM c"
        );

        var categories = new List<Category>();
        try
        {
            using var iterator = container.GetItemQueryIterator<Category>(
                query,
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(userId.ToString())                                                                      
                });
            
            while (iterator.HasMoreResults)
            {
                FeedResponse<Category> response = await iterator.ReadNextAsync();
                foreach (Category item in response)
                {
                    categories.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return categories;
    }

    public async Task<CategoryWithChildren?> GetCategoryWithChildren(Guid userId, Guid categoryId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        if (categoryId == Guid.Empty)
            throw new ArgumentException($"Category should not be null or empty: {categoryId}");

        var container = await GetTransactionsContainer();

        CategoryWithChildren? categoryWithChildren = null;
        try
        {
            var readItem = await container.ReadItemAsync<Category>(
                id: categoryId.ToString(),
                partitionKey: new PartitionKey(userId.ToString())
            );

            categoryWithChildren = await GetChildrenRecursivly(readItem.Resource, container);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }

        return categoryWithChildren;
    }

    private async Task<CategoryWithChildren> GetChildrenRecursivly(Category category, Container container)
    {
        var categoryWithChildren = category.ToCategoryWithChildren();
        
        var children = new List<Category>();
        
        var queryable = container.GetItemLinqQueryable<Category>();
        
        var categories = queryable
            .Where(p => p.ParentCategoryId == categoryWithChildren.CategoryId);

        using FeedIterator<Category> linqFeed = categories.ToFeedIterator();

        while (linqFeed.HasMoreResults)
        {
            FeedResponse<Category> response = await linqFeed.ReadNextAsync();
            
            // Iterate query results
            foreach (Category item in response)
            {
                children.Add(item);
            }
        }

        foreach (var child in children)
        {
            var childWithChildren = await GetChildrenRecursivly(child, container);
            categoryWithChildren.ChildrenCategories.Add(childWithChildren);
        }
        
        return categoryWithChildren;
    }

    public async Task<bool> RemoveCategory(Guid userId, Guid categoryId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        if (categoryId == Guid.Empty)
            throw new ArgumentException($"Category should not be null or empty: {categoryId}");
        
        var container = await GetTransactionsContainer();

        bool successfullyDeleted = false;
        try
        {
            var response = await container
                .DeleteItemAsync<Category>(
                    categoryId.ToString(), 
                    partitionKey: new PartitionKey(userId.ToString()));

            successfullyDeleted = response.StatusCode == HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        
        return successfullyDeleted;
    }

    public async Task<bool> RemoveCategories(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException($"UserId can't be null or empty: {userId}");
        
        var container = await GetTransactionsContainer();

        bool isSuccess = true;
        try
        {
            /*var result = await container
                .DeleteAllItemsByPartitionKeyStreamAsync(new PartitionKey(userId.ToString()));
            isSuccess = result.IsSuccessStatusCode;*/
            
            var iterator = container.GetItemQueryIterator<Category>(
                new QueryDefinition("SELECT * FROM c"),
                requestOptions: new QueryRequestOptions
                {
                    PartitionKey = new PartitionKey(userId.ToString())
                });

            while (iterator.HasMoreResults)
            {
                var page = await iterator.ReadNextAsync();
    
                foreach (var item in page)
                {
                    var response = await container.DeleteItemAsync<Category>(
                        item.CategoryId.ToString(),
                        new PartitionKey(userId.ToString())
                    );
                    isSuccess = isSuccess && response.StatusCode == HttpStatusCode.NoContent;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
        
        return isSuccess;
    }
    
    private async Task<Container> GetTransactionsContainer()
    {
        Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(
            id: "cost-manager-common-db", 
            ThroughputProperties.CreateAutoscaleThroughput(1000));

        Container container = await database.CreateContainerIfNotExistsAsync(
            id: "categories-container",
            partitionKeyPath: "/userId",
            throughput: 1000);

        return container;
    }
}