using Majority.RemittanceProvider.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public ICountryRepository Countries { get; }
        public UnitOfWork(ICountryRepository countryRepository)
        {
            Countries = countryRepository;
        }
    }
}
