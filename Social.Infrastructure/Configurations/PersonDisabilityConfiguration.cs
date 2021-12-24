using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities;

namespace Social.Infrastructure.Configurations
{
    public class PersonDisabilityConfiguration : IEntityTypeConfiguration<PersonDisability>
    {
        public void Configure(EntityTypeBuilder<PersonDisability> builder)
        {
            builder.ToTable("ss_person_disability");
        }
    }
}