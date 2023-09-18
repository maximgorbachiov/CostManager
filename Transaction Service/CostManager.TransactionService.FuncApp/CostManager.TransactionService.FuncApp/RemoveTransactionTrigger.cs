using System.Net;
using System.Threading.Tasks;
using CostManager.TransactionService.Abstracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
        [OpenApiOperation(operationId: "Run")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool),
            Description = "The OK response message containing a JSON result.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = null)] HttpRequest req, 
            ILogger log)
        {
            log.LogInformation($"C# HTTP trigger {nameof(RemoveTransactionTrigger)} processed a request.");

            string transactionId = req.Query["transactionId"].ToString();

            if (string.IsNullOrEmpty(transactionId))
            {
                log.LogError($"{nameof(RemoveTransactionTrigger)}: {nameof(transactionId)} should not be empty");
                return new BadRequestObjectResult($"{nameof(RemoveTransactionTrigger)}: {nameof(transactionId)} should not be empty");
            }

            bool removedSuccessfully = await _transactionRepository.RemoveTransactionAsync(transactionId);

            return new OkObjectResult(removedSuccessfully);
        }
    }
}
