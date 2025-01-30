using Azure.Core;
using Azure.Identity;
using CostManager.TransactionService.API.Extensions.Options;

namespace CostManager.TransactionService.API.Extensions
{
    public static class KeyVaultExtension
    {
        public static WebApplicationBuilder AddKeyVault(this WebApplicationBuilder builder)
        {
            // gets values from secret.json file
            // if app-registration-secret will be expired so should be created new one value for it and changed in secret.json
            var clientId = builder.Configuration.GetValue<string>("AppSettings:app-registration-id");
            var tenantId = builder.Configuration.GetValue<string>("AppSettings:azure-tenant-id");
            var clientSecret = builder.Configuration.GetValue<string>("AppSettings:app-registration-secret");

            var keyVaultSection = builder.Configuration.GetRequiredSection("KeyVault");
            var keyVaultOption = keyVaultSection.Get<KeyVaultOption>();

            try
            {
                if (!string.IsNullOrEmpty(keyVaultOption.Uri))
                {
                    var keyVaultUri = new Uri(keyVaultOption.Uri);

                    TokenCredential credential = builder.Environment.IsDevelopment() 
                        ? new ClientSecretCredential(tenantId, clientId, clientSecret) 
                        : new DefaultAzureCredential();

                    builder.Configuration.AddAzureKeyVault(keyVaultUri, credential);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return builder;
        }
    }
}
