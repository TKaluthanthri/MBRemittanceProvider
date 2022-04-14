using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Application.Interfaces
{
    public interface IBankRepository
    {
        Task<List<Bank>> GetAllBanksAsync();
        Task<Account> GetBeneficiaryName(string accoutNumber, string bankCode);
        Task<Bank> GetBankByCodeAsync(string code);
        Task<bool> SaveBankAccount(Account accountDetails);
    }
}
