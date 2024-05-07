using Azure.Identity;

namespace CostManager.TransactionService.API.Extensions
{
    public static class KeyVaultExtension
    {
        public static IConfigurationBuilder AddKeyVault(this IConfigurationBuilder configurationBuilder)
        {
            var configuration = configurationBuilder.Build();

            var keyVaultSection = configuration.GetSection("KeyVault");
            var keyVaultUri = new Uri(keyVaultSection["VaultUri"]);

            configurationBuilder.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());

            return configurationBuilder;
        }
    }
}
