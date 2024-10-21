using BusinessCard.Employments.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessCard.Infrastructure.Configurations.Employments
{
    public class EmploymentConfiguration : IEntityTypeConfiguration<Employment>
    {
        public void Configure(EntityTypeBuilder<Employment> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Assignments).WithOne(s => s.Employment).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.Employer).WithMany(s => s.Employments);

            builder.HasOne(s => s.Person).WithMany(s => s.Employments).HasForeignKey(s => s.PersonId);
        }
    }
}