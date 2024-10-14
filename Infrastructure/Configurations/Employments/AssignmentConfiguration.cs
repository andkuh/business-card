using BusinessCard.Employments.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessCard.Infrastructure.Configurations.Employments
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Duties);

            builder.HasOne(s => s.Employment).WithMany(s => s.Assignments);

            builder.Property(s => s.Name).HasMaxLength(256);

            builder.Property(s => s.Role).HasMaxLength(256);

            builder.Property(s => s.Description).HasMaxLength(2048);

            builder.Property(s => s.Summary).HasMaxLength(1024);
        }
    }
}