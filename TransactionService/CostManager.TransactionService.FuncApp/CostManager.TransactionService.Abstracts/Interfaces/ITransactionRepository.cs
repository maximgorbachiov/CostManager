using CostManager.TransactionService.Abstracts.Models;

namespace CostManager.TransactionService.Abstracts.Interfaces
{
    public interface ITransactionRepository
    {
        Task<string> AddTransactionAsync(AddTransactionModel addTransaction);

        Task<List<TransactionModel>> GetTransactionsListAsync();

        Task<bool> RemoveTransactionAsync(string transactionId);
    }
}
