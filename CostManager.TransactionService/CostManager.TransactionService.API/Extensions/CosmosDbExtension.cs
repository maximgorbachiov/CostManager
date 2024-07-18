using Microsoft.Azure.Cosmos;

namespace CostManager.TransactionService.API.Extensions
{
    public static class CosmosDbExtension
    {
        public static void AddCosmosDb(this IServiceCollection services)
        {
            services.AddSingleton(s =>
            {
                var configuration = services
                    .BuildServiceProvider()
                    .GetRequiredService<IConfiguration>();

                string connectionString = configuration.GetValue<string>("transactions-db-primaryConnectionString");

                return new CosmosClient(connectionString: connectionString);
            });
        }
    }
}
