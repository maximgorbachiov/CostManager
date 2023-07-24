using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;

namespace CostManager.TransactionService.DB
{
    public class TransactionRepository : ITransactionRepository
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public Guid AddTransaction(AddTransactionModel addTransaction)
        {
            var transaction = new Transaction
            {
                TransactionId = Guid.NewGuid(),
                Sum = addTransaction.Sum,
                PlaceOfTransaction = addTransaction.PlaceOfTransaction,
                Description = addTransaction.Description,
                TransactionDate = addTransaction.TransactionDate,
                CategoryId = addTransaction.CategoryId
            };

            _transactions.Add(transaction);

            return transaction.TransactionId;
        }

        public List<TransactionModel> GetTransactionsList()
        {
            var result = _transactions.Select(t => new TransactionModel
            {
                TransactionId = t.TransactionId,
                Sum = t.Sum,
                PlaceOfTransaction = t.PlaceOfTransaction,
                Description = t.Description,
                TransactionDate = t.TransactionDate,
                CategoryId = t.CategoryId
            }).ToList();

            return result;
        }
    }
}