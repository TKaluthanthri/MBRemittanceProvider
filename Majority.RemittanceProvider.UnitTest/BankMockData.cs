
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
            //List<Bank> output = new List<Bank>
            //{
            //    new Bank
            //    {
            //        Id = 1,
            //        BankCode = "WFBIUS6S",
            //        Name =  "Wells Fargo",
            //        Country_Id = "5"
            //    },
            //     new Bank
            //    {
            //        Id = 2,
            //        BankCode = "125200934",
            //        Name =  "Northrim bank",
            //        Country_Id = "5"
            //    },
            //      new Bank
            //    {
            //        Id = 3,
            //        BankCode = "DKW56257",
            //        Name =  "Jyske Bank",
            //        Country_Id = "5"
            //    },

            //};
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


    }
}
