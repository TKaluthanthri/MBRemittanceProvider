using Majority.RemittanceProvider.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Application.Interfaces
{
    public interface ICountryRepository 
    {
        Task<List<Country>> GetAllAsync();
        Task<List<State>> GetAllStatesAsync();
        Task<Country> GetByCodeAsync(string Name);
    }
}
