using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities;

namespace Social.Infrastructure.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("ss_person");
            builder.Property(t => t.FirstName)
                .HasMaxLength(100);
            builder.Property(t => t.LasName)
                .HasMaxLength(100);
            builder.Property(t => t.Image)
                .HasMaxLength(200);
        }
    }
}
