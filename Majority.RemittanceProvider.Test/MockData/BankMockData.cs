using Majority.RemittanceProvider.Test.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Test.MockData
{
    public class BankMockData
    {
        public static List<Bank> GetAllBanks()
        {
            return new List<Bank>
            {
               new Bank{
                 Id = 1,
                 Name = "Wells Fargo",
                 BankCode = "WFBIUS6S",
                 Country_Id = "5"
             },
             new Bank{
                  Id = 1,
                 Name = "Northrim bank",
                 BankCode = "WFBIUS6S",
                 Country_Id = "4"
             },
             new Bank{
                 Id = 1,
                 Name = "Jyske Bank",
                 BankCode = "DKW562572325325325",
                 Country_Id = "6"
             }
            };
        }
    }
}
