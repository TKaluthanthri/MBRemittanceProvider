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
        Task<TransactionInfo> GetTransactionStatusById(string transactionId);
        Task<TransactionInfo> GetTransactionStatusByTransactionNumber(string transactionNumber);
        Task<ExchangeRates> GetExchangeRates(string country);
        Task<List<ExchangeFee>> GetTransactionFeeAmount();
        Task<bool> SaveTransactionDetails(TransactionInfo transactionInfo);
    }
}
