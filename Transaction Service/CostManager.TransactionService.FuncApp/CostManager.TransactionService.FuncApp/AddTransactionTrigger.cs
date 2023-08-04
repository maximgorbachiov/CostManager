using System.IO;
using System.Threading.Tasks;
using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger AddTransaction function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var addTransactionModel = JsonConvert.DeserializeObject<AddTransactionModel>(requestBody);

            string newTransactionId = await _transactionRepository.AddTransaction(addTransactionModel);

            return new OkObjectResult(newTransactionId);
        }
    }
}
