using Dapper;
using Majority.RemittanceProvider.Application.Interfaces;
using Majority.RemittanceProvider.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConfiguration configuration;
        public TransactionRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<TransactionInfo> GetTransactionStatus(string transactionId)
        {
            var sql = "SELECT Id, TransactionNumber, Amount, ExchangeRate, Fees, CreatedDate, Status,Country, SenderId,ReceiverId FROM TransactionInfo  where TransactionNumber = '" + transactionId + "'";
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TransactionInfo>(sql);
                return result.FirstOrDefault();
                connection.Close();

            }
        }


        public async Task<ExchangeRates> GetExchangeRates(string country)
        {
            var sql = "SELECT Id, FromCountry, ToCountry, ExchangeRate, CurrencyType, LastUpdatedDate, ExchangeRateToken FROM ExchangeRates  where ToCountry = '" + country + "'";
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ExchangeRates>(sql);
                return result.FirstOrDefault();
                connection.Close();

            }

            
        }


        public async Task<List<ExchangeFee>> GetTransactionFeeAmount()
        {
            var sql = "SELECT Id,FromAmount,ToAmount,Fee,LastUpdatedDate FROM ExchangeFee";
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ExchangeFee>(sql);
                return result.ToList();
                connection.Close();

            }

            
        }
    }
}
