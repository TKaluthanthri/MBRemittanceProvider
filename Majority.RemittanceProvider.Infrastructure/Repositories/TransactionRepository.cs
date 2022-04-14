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
        private readonly IConfiguration _configuration;
        public TransactionRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<TransactionInfo> GetTransactionStatusById(string transactionId)
        {
            var sql = "SELECT Id, TransactionId,TransactionNumber, Amount, ExchangeRate, Fees, CreatedDate, Status,Country, SenderId,ReceiverId FROM TransactionInfo  where TransactionId = '" + transactionId + "'";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TransactionInfo>(sql);
                return result.FirstOrDefault();

            }
        }


        public async Task<TransactionInfo> GetTransactionStatusByTransactionNumber(string transactionNumber)
        {
            var sql = "SELECT Id, TransactionId,TransactionNumber, Amount, ExchangeRate, Fees, CreatedDate, Status,Country, SenderId,ReceiverId FROM TransactionInfo  where TransactionNumber = '" + transactionNumber + "'";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TransactionInfo>(sql);
                return result.FirstOrDefault();

            }
        }


        public async Task<ExchangeRates> GetExchangeRates(string country)
        {
            var sql = "SELECT Id, FromCountry, ToCountry, ExchangeRate, CurrencyType, LastUpdatedDate, ExchangeRateToken FROM ExchangeRates  where ToCountry = '" + country + "'";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ExchangeRates>(sql);
                return result.FirstOrDefault();

            }

        }


        public async Task<List<ExchangeFee>> GetTransactionFeeAmount()
        {
            var sql = "SELECT Id,FromAmount,ToAmount,Fee,LastUpdatedDate FROM ExchangeFee";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<ExchangeFee>(sql);
                return result.ToList();

            }

        }

        public async Task<bool> SaveTransactionDetails(TransactionInfo transactionInfo)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var isCreated = await connection.ExecuteAsync
                                        ("INSERT INTO TransactionInfo (TransactionNumber,TransactionId, Amount, ExchangeRate, Fees, CreatedDate, Status, Country, SenderId ,ReceiverId)" +
                                        " VALUES (@TransactionNumber,@TransactionId, @Amount, @ExchangeRate, @Fees, @CreatedDate, @Status, @Country, @SenderId ,@ReceiverId)",
                                        new
                                        {
                                            TransactionNumber = transactionInfo.TransactionNumber,
                                            TransactionId = transactionInfo.TransactionId,
                                            Amount = transactionInfo.Amount,
                                            ExchangeRate = transactionInfo.ExchangeRate,
                                            Fees = transactionInfo.Fees,
                                            CreatedDate = transactionInfo.CreatedDate,
                                            Status = transactionInfo.Status,
                                            Country = transactionInfo.Country,
                                            SenderId = transactionInfo.SenderId,
                                            ReceiverId = transactionInfo.ReceiverId

                                        }, transaction);

                        transaction.Commit();
                        if (isCreated == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }

    }
}
