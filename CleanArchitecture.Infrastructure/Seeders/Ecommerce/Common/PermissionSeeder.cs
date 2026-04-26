using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeders.Ecommerce.Common
{
    public class PermissionSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationEFCoreDbContext context)
        {

            // Insert modules with fixed IDs
            var insertDatas = new List<Permissions>
            {

                #region User Module #1
                new Permissions
                {
                    Id = 1,
                    Key = "users",
                    Position = 1,
                    DisplayName = "Users",
                    ModuleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Permissions
                {
                    Id = 2,
                    Key = "manage_users",
                    Position = 2,
                    DisplayName = "Manage Users",
                    ModuleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Permissions
                {
                    Id = 3,
                    Key = "manage_roles",
                    Position = 3,
                    DisplayName = "Manage Roles",
                    ModuleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                #endregion User Module #1
                
            };

            await context.Database.ExecuteSqlRawAsync("ALTER TABLE Modules NOCHECK CONSTRAINT ALL;");
            await context.Database.ExecuteSqlRawAsync("ALTER TABLE Permissions NOCHECK CONSTRAINT ALL;");
            await context.Database.ExecuteSqlRawAsync("ALTER TABLE RolePermission NOCHECK CONSTRAINT ALL;");

            // Remove all existing data
            var datas = await context.Permissions.ToListAsync();
            if (datas.Count > 0)
            {
                context.Permissions.RemoveRange(datas);
                await context.SaveChangesAsync();
            }

            await context.Permissions.AddRangeAsync(insertDatas);
            await context.SaveChangesAsync();

        }
    }
}
