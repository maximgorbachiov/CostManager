using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CostManager.TransactionService.API.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionServiceController : ControllerBase
    {
        private readonly ILogger<TransactionServiceController> _logger;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionServiceController(
            ILogger<TransactionServiceController> logger,
            ITransactionRepository transactionRepository)
        {
            _logger = logger;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TransactionModel>> GetAllTransactions()
        {
            _logger.LogInformation("Call to GetAllTransactions method");

            var allTransactions = await _transactionRepository.GetTransactionsListAsync();

            return allTransactions;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(AddTransactionModel addTransactionModel)
        {
            string infoMessage = new string($"C# HTTP method {nameof(GetAllTransactions)} processes request.");
            _logger.LogInformation(infoMessage);

            if (string.IsNullOrEmpty(addTransactionModel.CategoryId))
            {
                string errorMessage = new string($"{nameof(AddTransaction)}: {nameof(addTransactionModel.CategoryId)} should not be empty");
                _logger.LogError(errorMessage);

                return BadRequest(errorMessage);
            }

            var transactionId = await _transactionRepository.AddTransactionAsync(addTransactionModel);

            return Ok(transactionId);
        }

        [HttpDelete("{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(string transactionId)
        {
            string infoMessage = new string($"C# HTTP method {nameof(DeleteTransaction)} processes request.");
            _logger.LogInformation(infoMessage);

            if (string.IsNullOrEmpty(transactionId))
            {
                string errorMessage = $"{nameof(DeleteTransaction)}: {nameof(transactionId)} should not be empty";
                _logger.LogError(errorMessage);

                return BadRequest(errorMessage);
            }

            var isRemovedSuccessfully = await _transactionRepository.RemoveTransactionAsync(transactionId);

            return Ok(isRemovedSuccessfully);
        }
    }
}