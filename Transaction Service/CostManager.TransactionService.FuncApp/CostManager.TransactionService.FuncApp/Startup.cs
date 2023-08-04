using System.IO;
using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.DB;
using CostManager.TransactionService.FuncApp.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CostManager.TransactionService.FuncApp.Startup))]
namespace CostManager.TransactionService.FuncApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
            builder.AddCosmosDb();
            builder.Services.AddSingleton<ITransactionRepository, CosmosDbTransactionsRepository>();
        }

		public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
		{
			FunctionsHostBuilderContext context = builder.GetContext();

            builder.ConfigurationBuilder
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .AddKeyVault();
		}
	}
}
