using Dapper;
using Majority.RemittanceProvider.Application.Interfaces;
using Majority.RemittanceProvider.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Infrastructure.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly IConfiguration _configuration;

        public BankRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<List<Bank>> GetAllBanksAsync()
        {
            var sql = "SELECT Id, Name, BankCode, Country_Id FROM Bank";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Bank>(sql);
                return result.ToList();

            }
        }

        public async Task<Bank> GetBankByCodeAsync(string code)
        {
            var sql = "SELECT Id, Name, BankCode, Country_Id FROM Bank where BankCode = '" + code + "'";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Bank>(sql);
                return result.FirstOrDefault();

            }
        }


        public async Task<Account> GetBeneficiaryName(string accoutNumber, string bankCode)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();

                var result = await connection.QueryAsync<Account>(
                        "GetBeneficiaryName",
                        new { AccoutNumber = accoutNumber, BankCode = bankCode },
                        commandType: CommandType.StoredProcedure
                        );
                return result.FirstOrDefault();

            }

        }

        public async Task<bool> SaveBankAccount(Account accountDetails)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var isCreated = await connection.ExecuteAsync
                                        ("INSERT INTO Account (AccountNumber, BankAccountName, FirstName, LastName, CreatedDate, UpdatedDate, Bank_Id)" +
                                        " VALUES (@AccountNumber, @BankAccountName ,@FirstName, @LastName, @CreatedDate, @UpdatedDate, @Bank_Id)",
                                        new
                                        {
                                            AccountNumber = accountDetails.AccountNumber,
                                            BankAccountName = accountDetails.BankAccountName,
                                            FirstName = accountDetails.FirstName,
                                            LastName = accountDetails.LastName,
                                            CreatedDate = accountDetails.CreatedDate,
                                            UpdatedDate = accountDetails.UpdatedDate,
                                            Bank_Id = accountDetails.Bank_Id

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
