using System.Net;
using System.Threading.Tasks;
using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
        [OpenApiOperation(operationId: "Run")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(AddTransactionModel),
            Description = "JSON request body containing { hours, capacity}")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] AddTransactionModel addTransactionModel,
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
