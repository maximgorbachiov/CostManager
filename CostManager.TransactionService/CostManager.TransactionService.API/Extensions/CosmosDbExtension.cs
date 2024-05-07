namespace CostManager.TransactionService.API.Extensions
{
    public static class CosmosDbExtension
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
