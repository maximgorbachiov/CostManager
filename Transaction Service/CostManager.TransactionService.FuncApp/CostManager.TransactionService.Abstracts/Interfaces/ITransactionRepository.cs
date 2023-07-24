using CostManager.TransactionService.Abstracts.Models;

namespace CostManager.TransactionService.Abstracts.Interfaces
{
    public interface ITransactionRepository
    {
        Guid AddTransaction(AddTransactionModel addTransaction);

        List<TransactionModel> GetTransactionsList();
    }
}
