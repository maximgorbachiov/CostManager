using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostManager.TransactionService.DB
{
    public class Transaction
    {
        public string id { get; set; }
        public decimal Sum { get; set; }
        public string PlaceOfTransaction { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string partitionKey { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
