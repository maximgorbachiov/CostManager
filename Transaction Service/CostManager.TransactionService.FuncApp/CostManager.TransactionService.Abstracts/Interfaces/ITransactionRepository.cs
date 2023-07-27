using CostManager.TransactionService.Abstracts.Models;

namespace CostManager.TransactionService.Abstracts.Interfaces
{
    public interface ITransactionRepository
    {
        Task<string> AddTransaction(AddTransactionModel addTransaction);

        Task<List<TransactionModel>> GetTransactionsList();

        Task<bool> RemoveTransaction(string transactionId);
    }
}
