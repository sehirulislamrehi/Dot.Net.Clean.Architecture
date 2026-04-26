using CleanArchitecture.Infrastructure.DatabaseContext;
using CleanArchitecture.Infrastructure.Seeders.Ecommerce.Common;
using CleanArchitecture.Infrastructure.Seeders.Ecommerce.Users;
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

        public static IServiceCollection AddDatabaseSeeders(this IServiceCollection services)
        {
            /* comments which seeder classes are unnecessary to run */

            services.AddScoped<ISeeder, ModuleSeeder>();
            services.AddScoped<ISeeder, SubModuleSeeder>();
            services.AddScoped<ISeeder, PermissionSeeder>();
            //services.AddScoped<ISeeder, UserSeeder>();

            return services;
        }
    }
}
