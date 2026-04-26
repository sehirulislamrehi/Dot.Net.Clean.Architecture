using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using CleanArchitecture.Domain.Ecommerce.IRepository.CommonModule;
using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Ecommerce.Repository.CommonModule
{
    public class ModuleRepository : IModuleRepository
    {
        private ApplicationEFCoreDbContext _dbContext;
        public ModuleRepository(
            ApplicationEFCoreDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Modules>> GetModules()
        {
            var query = _dbContext.Modules
                .Include(query => query.SubModules)
                .Include(query => query.Permissions)
                .AsQueryable();
            return query.OrderByDescending(u => u.Id);
        }
    }
}
