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
    public class SubModuleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationEFCoreDbContext context)
        {

            // Insert modules with fixed IDs
            var subModules = new List<SubModules>
            {

                #region User Module #1
                new SubModules
                {
                    Id = 1,
                    Name = "Manage Users",
                    Key = "manage_users",
                    Position = 1,
                    Route = "/admin/user-module/user",
                    ModuleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new SubModules
                {
                    Id = 2,
                    Name = "Manage Roles",
                    Key = "manage_roles",
                    Position = 2,
                    Route = "/admin/user-module/role",
                    ModuleId = 1,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
                #endregion User Module #1

            };

            await context.Database.ExecuteSqlRawAsync("ALTER TABLE Modules NOCHECK CONSTRAINT ALL;");
            await context.Database.ExecuteSqlRawAsync("ALTER TABLE Permissions NOCHECK CONSTRAINT ALL;");
            await context.Database.ExecuteSqlRawAsync("ALTER TABLE RolePermission NOCHECK CONSTRAINT ALL;");

            // Remove all existing data
            var allSubModules = await context.SubModules.ToListAsync();
            if (allSubModules.Count > 0)
            {
                context.SubModules.RemoveRange(allSubModules);
                await context.SaveChangesAsync();
            }

            await context.SubModules.AddRangeAsync(subModules);
            await context.SaveChangesAsync();

        }
    }
}
