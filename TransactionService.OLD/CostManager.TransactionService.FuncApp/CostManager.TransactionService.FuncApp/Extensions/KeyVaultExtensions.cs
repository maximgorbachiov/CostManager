using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace CostManager.TransactionService.FuncApp.Extensions
{
	public static class KeyVaultExtensions
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
