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
    public class ModuleSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationEFCoreDbContext context)
        {

            // Insert modules with fixed IDs
            var modules = new List<Modules>
                {
                    new Modules
                    {
                        Id = 1,
                        Name = "Dashboard",
                        Key = "dashboard",
                        Icon = "dashboard",
                        Position = 1,
                        Route = "/dashboard",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    },
                    new Modules
                    {
                        Id = 2,
                        Name = "Users",
                        Key = "users",
                        Icon = "users",
                        Position = 2,
                        Route = null,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                };

            await context.Database.ExecuteSqlRawAsync("ALTER TABLE Modules NOCHECK CONSTRAINT ALL;");
            await context.Database.ExecuteSqlRawAsync("ALTER TABLE Permissions NOCHECK CONSTRAINT ALL;");
            await context.Database.ExecuteSqlRawAsync("ALTER TABLE RolePermission NOCHECK CONSTRAINT ALL;");

            // Remove all existing data
            var allModules = await context.Modules.ToListAsync();
            if (allModules.Count > 0)
            {
                context.Modules.RemoveRange(allModules);
                await context.SaveChangesAsync();
            }

            await context.Modules.AddRangeAsync(modules);
            await context.SaveChangesAsync();

        }

    }
}
