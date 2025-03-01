using Newtonsoft.Json;

namespace CostManager.TransactionService.DB
{
    public class Transaction
    {
        [JsonProperty("id")]
        public string TransactionId { get; set; }
        
        public decimal Sum { get; set; }
        public string PlaceOfTransaction { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
        
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
