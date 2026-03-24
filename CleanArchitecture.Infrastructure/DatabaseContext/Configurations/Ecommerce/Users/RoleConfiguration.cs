using CleanArchitecture.Domain.Ecommerce.Entities.Common;
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
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.IsActive)
                .HasDefaultValue(false);

            builder.Property(r => r.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(r => r.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
