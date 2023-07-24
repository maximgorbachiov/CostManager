using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostManager.TransactionService.DB
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public decimal Sum { get; set; }
        public string PlaceOfTransaction { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
