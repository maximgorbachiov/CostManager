using CostManager.TransactionService.Abstracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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
            Guid transactionGuid = Guid.Parse(transactionId);

            bool removedSuccessfully = _transactionRepository.RemoveTransaction(transactionGuid);

            return new OkObjectResult(removedSuccessfully);
        }
    }
}
