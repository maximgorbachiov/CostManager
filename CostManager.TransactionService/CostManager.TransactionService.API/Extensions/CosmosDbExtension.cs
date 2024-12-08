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

                string connectionString = configuration.GetValue<string>("cost-manager-common-db-connection-string");

                return new CosmosClient(connectionString: connectionString);
            });
        }
    }
}
