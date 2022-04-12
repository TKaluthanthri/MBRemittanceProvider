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
    public class CountryRepository : ICountryRepository
    {
        private readonly IConfiguration configuration;
        public CountryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            var sql = "SELECT * FROM Country";
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Country>(sql);
                return result.ToList();
                connection.Close();

            }
        }

        public async Task<List<State>> GetAllStatesAsync()
        {
            var sql = "SELECT * FROM State";
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<State>(sql);
                return result.ToList();
                connection.Close();

            }
        }

        public async Task<Country> GetByCodeAsync(string Name)
        {
            var sql = "SELECT * FROM Country where Name = " + Name + "";
            using (var connection = new SqlConnection(configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Country>(sql);
                return result.SingleOrDefault();
                connection.Close();

            }
        }


    }
}
