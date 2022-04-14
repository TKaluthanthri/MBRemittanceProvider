using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Majority.RemittanceProvider.Core.Entities
{
    public class TransactionInfo
    {
        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fees { get; set; }
        public string ExchangeRate { get; set; }
        public DateTime CreatedDate { get; set; }
        public TransactionStatus Status { get; set; }
        public string Country { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

    }
}
