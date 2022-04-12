using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Application.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByCodeAsync(string code);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllStatesAsync();
    }
}
