using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities;

namespace Social.Infrastructure.Configurations
{
    public class PersonHobbyConfiguration : IEntityTypeConfiguration<PersonHobby>
    {
        public void Configure(EntityTypeBuilder<PersonHobby> builder)
        {
            builder.ToTable("ss_person_hobby");
        }
    }
}