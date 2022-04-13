using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Core.Entities
{
    public class ExchangeRates
    {
        public int Id { get; set; }
        public string FromCountry { get; set; }
        public string ToCountry { get; set; }
        public decimal ExchangeRate { get; set; }
        public string CurrencyType { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public string ExchangeRateToken { get; set; }
        
    }
}
