using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionInfo> GetTransactionStatus(string transactionId);
        Task<ExchangeRates> GetExchangeRates(string country);
        Task<List<ExchangeFee>> GetTransactionFeeAmount();
    }
}
