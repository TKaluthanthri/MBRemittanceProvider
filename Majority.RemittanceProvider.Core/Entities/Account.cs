using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string BankAccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Bank_Id { get; set; }
    }
}
