using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.UnitTest.MockData
{
    public class CountryMockData
    {
        public static async Task<List<Country>> GetCountryList()
        {
            return await Task.FromResult(new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name =  "Albania",
                    Code = "AL"
                },
                 new Country
                {
                    Id = 2,
                    Name =  "Denmark",
                    Code = "DK"
                },
                  new Country
                {
                    Id = 3,
                    Name =  "Sweden",
                    Code = "SE"
                },

            });
        }

        public static async Task<List<Country>> GetEmptyCountryList()
        {
            return await Task.FromResult(new List<Country>());
        }

    }
}
