using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Infrastructure.DatabaseContext.Configurations.Ecommerce.Common
{
    public class SubModuleConfiguration : IEntityTypeConfiguration<SubModule>
    {
        public void Configure(EntityTypeBuilder<SubModule> builder)
        {
            // Table name
            builder.ToTable("SubModules");

            // Primary Key
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            // Fields
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Position)
                .IsRequired();

            builder.Property(x => x.Route)
                .HasMaxLength(250);

            builder.Property(x => x.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.UpdatedAt);

            // Relationship (Many SubModules → One Module)
            builder.HasOne(x => x.Module)
                .WithMany(m => m.SubModules) // Ensure Modules entity has ICollection<SubModules>
                .HasForeignKey(x => x.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
