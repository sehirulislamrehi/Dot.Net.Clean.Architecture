using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using CleanArchitecture.Infrastructure.DatabaseContext.Configurations.Ecommerce.Common;
using CleanArchitecture.Infrastructure.DatabaseContext.Configurations.Ecommerce.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CleanArchitecture.Infrastructure.DatabaseContext
{
    public class ApplicationEFCoreDbContext : DbContext
    {
        public ApplicationEFCoreDbContext(DbContextOptions<ApplicationEFCoreDbContext> options) : base(options) { }

        #region Common Module DbSet
        public DbSet<Modules> Modules { get; set; }
        public DbSet<SubModules> SubModules { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        #endregion Common Module DbSet End

        #region User Module DbSet
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        #endregion User Module DbSet End

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Common Module DBConfiguration
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new SubModuleConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            #endregion Common Module DBConfiguration End

            #region User Module DBConfiguration
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            #endregion User Module DBConfiguration End

            base.OnModelCreating(modelBuilder);
        }
    }
}
