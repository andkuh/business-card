using BusinessCard.People.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessCard.Infrastructure.Configurations.People
{
    public class PersonConfiguration: IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasMany(s => s.Employments);

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Location).HasMaxLength(512);

            builder.Property(s => s.Summary).HasMaxLength(2048);

            builder.Property(s => s.Specialization).HasMaxLength(128);

            builder.Property(s => s.FirstName).HasMaxLength(256);

            builder.Property(s => s.LastName).HasMaxLength(256);

            builder.OwnsOne(s => s.Image);
        }
    }
}