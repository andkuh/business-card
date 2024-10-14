using BusinessCard.Technologies.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessCard.Infrastructure.Configurations.Technologies
{
    public class TechnologyConfiguration: IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Title).HasMaxLength(256);
        }
    }
}