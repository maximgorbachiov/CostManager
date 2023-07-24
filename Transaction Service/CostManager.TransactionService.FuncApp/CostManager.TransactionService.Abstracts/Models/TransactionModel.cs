namespace CostManager.TransactionService.Abstracts.Models
{
    public class TransactionModel
    {
        public Guid TransactionId { get; set; }
        public decimal Sum { get; set; }
        public string PlaceOfTransaction { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
