using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.DB;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CostManager.TransactionService.FuncApp.Startup))]
namespace CostManager.TransactionService.FuncApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ITransactionRepository, TransactionRepository>();
        }
    }
}
