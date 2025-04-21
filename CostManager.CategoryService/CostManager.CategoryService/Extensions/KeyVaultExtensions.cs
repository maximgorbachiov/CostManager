using Azure.Identity;
using CostManager.CategoryService.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CostManager.CategoryService.Extensions;

public static class KeyVaultExtensions
{
    public static void AddKeyVault(this IConfigurationBuilder configurationBuilder)
    {
        var tempConfig = configurationBuilder.Build();
        var keyVaultSection = tempConfig.GetSection("KeyVault");
        var keyVaultOption = keyVaultSection.Get<KeyVaultOption>();
        var keyVaultUri = new Uri(keyVaultOption.VaultUri);

        configurationBuilder.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
    }
}