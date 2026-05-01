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
        public DbSet<Domain.Ecommerce.Entities.Common.Module> Modules { get; set; }
        public DbSet<SubModule> SubModules { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        #endregion Common Module DbSet End

        #region User Module DbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
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
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            #endregion User Module DBConfiguration End

            base.OnModelCreating(modelBuilder);
        }
    }
}
