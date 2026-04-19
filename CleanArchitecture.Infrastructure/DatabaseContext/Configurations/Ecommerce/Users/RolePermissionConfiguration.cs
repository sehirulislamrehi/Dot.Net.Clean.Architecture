using CleanArchitecture.Domain.Ecommerce.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.DatabaseContext.Configurations.Ecommerce.Users
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.RoleId)
                .IsRequired();

            builder.Property(r => r.PermissionId)
                .IsRequired();

        }
    }
}
