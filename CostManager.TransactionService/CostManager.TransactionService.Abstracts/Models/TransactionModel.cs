﻿namespace CostManager.TransactionService.Abstracts.Models
{
    public class TransactionModel
    {
        public string TransactionId { get; set; }
        public decimal Sum { get; set; }
        public string PlaceOfTransaction { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string UserId { get; set; }
    }
}
