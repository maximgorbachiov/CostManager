using System.Threading.Tasks;
using CostManager.TransactionService.Abstracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CostManager.TransactionService.FuncApp
{
    public class GetTransactionsListTrigger
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionsListTrigger(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [FunctionName("GetTransactionsListTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger {nameof(GetTransactionsListTrigger)} processed a request.");

            var transactionModels = await _transactionRepository.GetTransactionsListAsync();

            return new OkObjectResult(transactionModels);
        }
    }
}
