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

                string endpoint = configuration.GetValue<string>("transactions-cosmosdb-endpoint");
                string authToken = configuration.GetValue<string>("transactions-cosmosdb-authtoken");

                return new CosmosClient(
                    accountEndpoint: endpoint,
                    authKeyOrResourceToken: authToken
                );
            });
        }
    }
}
