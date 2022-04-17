using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.UnitTest.MockData
{
    public class TransactionMockData
    {

        public static async Task<ExchangeRates> GetExchangeRates()
        {
            return await Task.FromResult(new ExchangeRates
            {

                Id = 1,
                FromCountry = "US",
                ToCountry = "SE",
                ExchangeRate = 9.522m,
                CurrencyType = "SEK",
                LastUpdatedDate = DateTime.Now,
                ExchangeRateToken = "SEK9879659023420"


            });
        }
    }
}
