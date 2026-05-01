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
        public async Task<IEnumerable<Module>> GetModules()
        {
            var query = _dbContext.Modules
                .Include(query => query.SubModules)
                .Select(query => new Module { 
                    Id = query.Id,
                    Name = query.Name,
                    Key = query.Key,
                    Icon = query.Icon,
                    Position = query.Position,
                    Route = query.Route,
                    CreatedAt = query.CreatedAt,
                    Permissions = query.Permissions.OrderBy(permission => permission.Position).ToList(),
                })
                .OrderByDescending(query => query.Id)
                .AsQueryable();
            return query.OrderByDescending(u => u.Id);
        }
    }
}
