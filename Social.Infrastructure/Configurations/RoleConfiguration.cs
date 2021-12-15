using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities.Auth;

namespace Social.Infrastructure.Configurations
{
   public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("ss_role");
            builder.Property(t => t.Name)
            .HasMaxLength(50)
            .IsRequired();
            builder.Property(t => t.Description)
            .HasMaxLength(200);
        }
    }
}
