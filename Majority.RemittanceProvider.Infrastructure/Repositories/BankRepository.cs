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
                connection.Close();

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
                connection.Close();

            }
        }
    }
}
