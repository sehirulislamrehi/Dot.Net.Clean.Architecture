using Azure.Core;
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
            var query = _dbContext.Roles
                .AsQueryable();

            if (queryParam.HasValue)
            {
                var param = queryParam.Value;
                if (param.TryGetProperty("Name", out var name))
                {
                    var nameValue = name.GetString();
                    if (!string.IsNullOrEmpty(nameValue))
                    {
                        query = query.Where(u => u.Name.Contains(nameValue));
                    }
                }
            }

            return query.OrderByDescending(u => u.Id);
        }

        
        public async Task<Role?> GetRoleById(int id)
        {
            return await _dbContext.Roles
                .Include(query => query.RolePermissions)
                    .ThenInclude(rolePermission => rolePermission.Permission)
                .FirstOrDefaultAsync(query => query.Id == id);
        }

        public async Task<Role> CreateRole(Role role)
        {
            _dbContext.Roles.Add(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

        public async Task<Role> EditRole(Role role, List<int> permissionIDs)
        {

            /* remove all existing role permission */
            _dbContext.RolePermissions.RemoveRange(role.RolePermissions);

            role.RolePermissions = permissionIDs.Select(id => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = id
            }).ToList();

            _dbContext.Roles.Update(role);
            await _dbContext.SaveChangesAsync();
            return role;
        }

    }
}
