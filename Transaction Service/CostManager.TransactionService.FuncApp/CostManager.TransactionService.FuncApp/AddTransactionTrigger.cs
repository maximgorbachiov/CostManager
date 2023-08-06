using System.Threading.Tasks;
using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace CostManager.TransactionService.FuncApp
{
    public class AddTransactionTrigger
    {
        private readonly ITransactionRepository _transactionRepository;

        public AddTransactionTrigger(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [FunctionName("AddTransactionTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] AddTransactionModel addTransactionModel,
            ILogger log)
        {
            string infoMessage = new string($"C# HTTP trigger {nameof(AddTransactionTrigger)} processed a request.");
            log.LogInformation(infoMessage);

            if (string.IsNullOrEmpty(addTransactionModel.CategoryId))
            {
                string errorMessage = new string($"{nameof(AddTransactionTrigger)}: {nameof(addTransactionModel.CategoryId)} should not be empty");
                log.LogError(errorMessage);
                return new BadRequestObjectResult(errorMessage);
            }

            string newTransactionId = await _transactionRepository.AddTransactionAsync(addTransactionModel);

            return new OkObjectResult(newTransactionId);
        }
    }
}
