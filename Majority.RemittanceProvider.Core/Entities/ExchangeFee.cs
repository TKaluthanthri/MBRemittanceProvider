using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Core.Entities
{
    public class ExchangeFee
    {
        public int Id { get; set; }
        public decimal FromAmount { get; set; }
        public decimal ToAmount { get; set; }
        public decimal Fee { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
