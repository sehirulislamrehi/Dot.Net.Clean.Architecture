using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Ecommerce.IRepository.CommonModule
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetModules();
    }
}
