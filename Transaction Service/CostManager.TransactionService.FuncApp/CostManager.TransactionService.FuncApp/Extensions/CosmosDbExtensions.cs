using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace CostManager.TransactionService.FuncApp.Extensions
{
    public static class CosmosDbExtensions
    {
        public static void AddCosmosDb(this IServiceCollection services)
        {
            services.AddSingleton(s =>
            {
                return new CosmosClient(
                    accountEndpoint: "https://transactions-storage.documents.azure.com:443",
                    authKeyOrResourceToken: "kR5lrifRmEr29PAXtlh7N2rViTJC2FZYYTEC79EvosAXsljUDuBbLlPgQidjr6OT69datJsDZf6nACDbljDnLA=="
                );
            });
        }
    }
}
