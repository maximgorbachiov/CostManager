using System.Threading.Tasks;
using CostManager.TransactionService.Abstracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CostManager.TransactionService.FuncApp
{
    public class RemoveTransactionTrigger
    {
        private readonly ITransactionRepository _transactionRepository;

        public RemoveTransactionTrigger(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [FunctionName("RemoveTransactionTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger RemoveTransaction function processed a request.");

            string transactionId = req.Query["transactionId"].ToString();

            bool removedSuccessfully = await _transactionRepository.RemoveTransaction(transactionId);

            return new OkObjectResult(removedSuccessfully);
        }
    }
}
