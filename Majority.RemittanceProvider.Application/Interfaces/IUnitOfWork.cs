using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Majority.RemittanceProvider.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICountryRepository Countries { get; }
    }
}
