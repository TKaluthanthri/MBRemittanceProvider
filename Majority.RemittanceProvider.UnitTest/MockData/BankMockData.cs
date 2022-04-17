
using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.UnitTest
{
    public class BankMockData
    {
        public static async Task<List<Bank>> GetSampleBankList()
        {
            return await Task.FromResult(new List<Bank>
            {
                new Bank
                {
                    Id = 1,
                    BankCode = "WFBIUS6S",
                    Name =  "Wells Fargo",
                    Country_Id = "5"
                },
                 new Bank
                {
                    Id = 2,
                    BankCode = "125200934",
                    Name =  "Northrim bank",
                    Country_Id = "5"
                },
                  new Bank
                {
                    Id = 3,
                    BankCode = "DKW56257",
                    Name =  "Jyske Bank",
                    Country_Id = "5"
                },

            });
        }


        public static async Task<List<Bank>> GetEmptySampleBankList()
        {
            return await Task.FromResult(new List<Bank>());
        }

        public static async Task<Bank> GetBankByCodeAsync()
        {
            return await Task.FromResult(new Bank
            {
                Id = 3,
                BankCode = "DKW56257",
                Name = "Jyske Bank",
                Country_Id = "5"

            });
        }

    }
}
