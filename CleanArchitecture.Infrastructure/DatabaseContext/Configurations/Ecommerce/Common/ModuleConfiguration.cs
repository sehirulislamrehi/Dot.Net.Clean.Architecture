using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.DatabaseContext.Configurations.Ecommerce.Common
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {

        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            builder.Property(module => module.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(module => module.Key)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(module => module.Key)
                .IsUnique();

            builder.Property(module => module.Icon)
                .HasMaxLength(100);

            builder.Property(module => module.Position)
                .IsRequired();

            builder.Property(module => module.Route)
                .HasMaxLength(500);

            builder.Property(module => module.CreatedAt)
                .IsRequired();

            builder.Property(module => module.UpdatedAt)
                .IsRequired();
        }
    }
}
