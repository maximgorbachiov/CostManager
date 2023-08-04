using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CostManager.TransactionService.FuncApp.Extensions
{
    public static class CosmosDbExtensions
    {
        public static void AddCosmosDb(this IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(s =>
            {
				var configuration = builder.Services
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
