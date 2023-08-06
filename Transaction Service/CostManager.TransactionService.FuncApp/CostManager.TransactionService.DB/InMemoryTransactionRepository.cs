using CostManager.TransactionService.Abstracts.Interfaces;
using CostManager.TransactionService.Abstracts.Models;

namespace CostManager.TransactionService.DB
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public async Task<string> AddTransactionAsync(AddTransactionModel addTransaction)
        {
            var transaction = new Transaction
            {
                id = Guid.NewGuid().ToString(),
                Sum = addTransaction.Sum,
                PlaceOfTransaction = addTransaction.PlaceOfTransaction,
                Description = addTransaction.Description,
                TransactionDate = addTransaction.TransactionDate,
                CategoryId = addTransaction.CategoryId
            };

            _transactions.Add(transaction);

            return await Task.FromResult(transaction.id);
        }

        public async Task<List<TransactionModel>> GetTransactionsListAsync()
        {
            var result = _transactions.Select(t => new TransactionModel
            {
                TransactionId = t.id,
                Sum = t.Sum,
                PlaceOfTransaction = t.PlaceOfTransaction,
                Description = t.Description,
                TransactionDate = t.TransactionDate,
                CategoryId = t.CategoryId
            }).ToList();

            return await Task.FromResult(result);
        }

        public async Task<bool> RemoveTransactionAsync(string transactionId)
        {
            var transaction = _transactions.FirstOrDefault(t => t.id == transactionId);
            
            bool result = transaction != null;            

            if (result)
            {
                _transactions.Remove(transaction);
            }

            return await Task.FromResult(result);
        }
    }
}