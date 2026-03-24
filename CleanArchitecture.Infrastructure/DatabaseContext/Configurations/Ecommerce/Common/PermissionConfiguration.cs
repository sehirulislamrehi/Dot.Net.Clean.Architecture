using CleanArchitecture.Domain.Ecommerce.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.DatabaseContext.Configurations.Ecommerce.Common
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> builder)
        {
            // Table name
            builder.ToTable("Permissions");

            // Primary Key
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedNever();

            // Columns
            builder.Property(p => p.Position)
                .IsRequired();

            builder.Property(p => p.Key)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.DisplayName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedAt);

            // Relationship: Many Permissions → One Module
            builder.HasOne(p => p.Module)
                .WithMany(m => m.Permissions) // Ensure Modules has ICollection<Permissions>
                .HasForeignKey(p => p.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
