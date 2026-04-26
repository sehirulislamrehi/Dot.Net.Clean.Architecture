using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Domain.Ecommerce.IRepository.UserModule.Roles;
using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Ecommerce.Repository.UserModule.Roles
{
    public class RoleRepository : IRoleRepository
    {
        private ApplicationEFCoreDbContext _dbContext;
        public RoleRepository(
            ApplicationEFCoreDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Role>> GetAllRole(JsonElement? queryParam = null)
        {
            var query = _dbContext.Role
                .AsQueryable();
            return query.OrderByDescending(u => u.Id);
        }

    }
}
