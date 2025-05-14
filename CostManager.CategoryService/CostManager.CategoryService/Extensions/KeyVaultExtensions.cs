using Azure.Identity;
using CostManager.CategoryService.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace CostManager.CategoryService.Extensions;

public static class KeyVaultExtensions
{
    public static void AddKeyVault(this IConfigurationBuilder configurationBuilder)
    {
        var tempConfig = configurationBuilder.Build();
        var keyVaultSection = tempConfig.GetSection("AzureKeyVault");
        var keyVaultOption = keyVaultSection.Get<KeyVaultOption>();
        
        if (keyVaultOption == null)
            throw new InvalidOperationException("Missing AzureKeyVault configuration.");
        
        if (string.IsNullOrEmpty(keyVaultOption.VaultUri))
            throw new InvalidOperationException("Missing AzureKeyVault:VaultUri configuration.");
        
        var keyVaultUri = new Uri(keyVaultOption.VaultUri);

        configurationBuilder.AddAzureKeyVault(keyVaultUri, new DefaultAzureCredential());
    }
}