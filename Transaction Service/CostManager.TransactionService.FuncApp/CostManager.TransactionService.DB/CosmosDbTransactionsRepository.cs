using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace CostManager.TransactionService.DB
{
    public class CosmosDbTransactionsRepository : ITransactionRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly ILogger _logger;

        public CosmosDbTransactionsRepository(
            CosmosClient cosmosClient, 
            ILogger<CosmosDbTransactionsRepository> logger)
        {
            _cosmosClient = cosmosClient;
            _logger = logger;
        }

        public async Task<string> AddTransaction(AddTransactionModel addTransaction)
        {
            var container = await GetTransactionsContainer();

            var transaction = new Transaction
            {
                id = Guid.NewGuid().ToString(),
                Sum = addTransaction.Sum,
                PlaceOfTransaction = addTransaction.PlaceOfTransaction,
                Description = addTransaction.Description,
                TransactionDate = addTransaction.TransactionDate,
                CategoryId = addTransaction.CategoryId,
                partitionKey = "const" // should do some research about this option and changed in future
            };

            Transaction createdItem = null;

            try
            {
                createdItem = await container.CreateItemAsync(
                    item: transaction,
                    partitionKey: new PartitionKey(transaction.partitionKey)
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return createdItem?.id ?? string.Empty;
        }

        public async Task<List<TransactionModel>> GetTransactionsList()
        {
            var result = new List<TransactionModel>();

            var container = await GetTransactionsContainer();

            var query = new QueryDefinition(
                query: "SELECT * FROM transactionsList t"
            );

            using FeedIterator<Transaction> iterator = container.GetItemQueryIterator<Transaction>(queryDefinition: query);

            while (iterator.HasMoreResults)
            {
                FeedResponse<Transaction> response = await iterator.ReadNextAsync();

                result.AddRange(response.Select(t => new TransactionModel
                {
                    TransactionId = t.id,
                    Sum = t.Sum,
                    PlaceOfTransaction = t.PlaceOfTransaction,
                    Description = t.Description,
                    TransactionDate = t.TransactionDate,
                    CategoryId = t.CategoryId
                }).ToList());
            }

            return result;
        }

        public async Task<bool> RemoveTransaction(string transactionId)
        {
            var container = await GetTransactionsContainer();

            try
            {
                _ = await container.DeleteItemAsync<Transaction>(
                    id: transactionId,
                    partitionKey: new PartitionKey("const"));

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<Container> GetTransactionsContainer()
        {
            Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(id: "TransactionsStorageDB");
            Container container = await database.CreateContainerIfNotExistsAsync(
                id: "transactionsList",
                partitionKeyPath: "/partitionKey",
                throughput: 1000);

            return container;
        }
    }
}
