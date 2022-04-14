using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Application.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByEmail(string Email);
        Task<bool> SaveCustomer(Customer customer);
    }
}
