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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        public CustomerRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task<Customer> GetCustomerByEmail(string Email)
        {
            var sql = "SELECT * FROM Customer where  Email = '" + Email + "'";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Customer>(sql);
                return result.FirstOrDefault();
            }

        }

        public async Task<bool> SaveCustomer(Customer customer)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {

                        var isCreated = await connection.ExecuteAsync
                                        ("INSERT INTO Customer (FirstName, LastName, PhoneNumber, AddressLine, Country, City, State, PostalCode, dateOfBirth, Email, CreatedDate, CustomerCode)" +
                                        " VALUES (@FirstName, @LastName, @PhoneNumber, @AddressLine,@Country,@City, @State, @PostalCode, @dateOfBirth,@Email, @CreatedDate,@CustomerCode )",
                                        new
                                        {
                                            FirstName = customer.FirstName,
                                            LastName = customer.LastName,
                                            PhoneNumber = customer.PhoneNumber,
                                            AddressLine = customer.AddressLine,
                                            Country = customer.Country,
                                            City = customer.City,
                                            State = customer.State,
                                            PostalCode = customer.PostalCode,
                                            dateOfBirth = customer.dateOfBirth,
                                            Email = customer.Email,
                                            CreatedDate = customer.CreatedDate,
                                            CustomerCode = customer.CustomerCode

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
