using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities;

namespace Social.Infrastructure.Configurations
{
    public class DisabilityConfiguration : IEntityTypeConfiguration<Disability>
    {
        public void Configure(EntityTypeBuilder<Disability> builder)
        {
            builder.ToTable("ss_disability");
            builder.Property(t => t.Name)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(t => t.Icon)
                .HasMaxLength(100);
            builder.Property(t => t.Description)
                .HasMaxLength(100);
        }
    }
}
