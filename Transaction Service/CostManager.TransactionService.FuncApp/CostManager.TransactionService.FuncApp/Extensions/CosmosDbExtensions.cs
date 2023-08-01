using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CostManager.TransactionService.FuncApp.Extensions
{
    public static class CosmosDbExtensions
    {
        public static void AddCosmosDb(this IServiceCollection services)
        {
            services.AddSingleton(s =>
            {
                SecretClientOptions options = new SecretClientOptions()
                {
                    Retry =
                    {
                        Delay= TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
                };

                var client = new SecretClient(new Uri("https://secrets-storage.vault.azure.net/"), new DefaultAzureCredential(), options);

                KeyVaultSecret endpoint = client.GetSecret("transactions-cosmosdb-endpoint");
                KeyVaultSecret authToken = client.GetSecret("transactions-cosmosdb-authtoken");

                return new CosmosClient(
                    accountEndpoint: endpoint.Value,
                    authKeyOrResourceToken: authToken.Value
                );
            });
        }
    }
}
