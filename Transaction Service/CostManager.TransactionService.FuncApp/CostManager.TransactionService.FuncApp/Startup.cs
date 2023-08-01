using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.DB;
using CostManager.TransactionService.FuncApp.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CostManager.TransactionService.FuncApp.Startup))]
namespace CostManager.TransactionService.FuncApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddSingleton<ITransactionRepository, InMemoryTransactionRepository>();
            builder.Services.AddCosmosDb();
            builder.Services.AddSingleton<ITransactionRepository, CosmosDbTransactionsRepository>();
        }
    }
}
