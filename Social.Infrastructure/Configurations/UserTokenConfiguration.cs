using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities.Auth;

namespace Social.Infrastructure.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("ss_user_token");
            builder.Property(t => t.CreationDate)
                .IsRequired();
            builder.Property(t => t.ValidUntil)
                .IsRequired();
            builder.Property(t => t.Token)
                .HasMaxLength(5)
                .IsRequired();
        }
    }
}
