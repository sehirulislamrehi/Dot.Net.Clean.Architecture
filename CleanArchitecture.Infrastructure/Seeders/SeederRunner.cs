using CleanArchitecture.Infrastructure.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Seeders
{
    public static class SeederRunner
    {
        public static async Task RunSeedersAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<ISeeder>();

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(scope.ServiceProvider.GetRequiredService<ApplicationEFCoreDbContext>());
            }
        }
    }
}
