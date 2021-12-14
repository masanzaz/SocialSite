using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Social.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Infrastructure.Configurations
{
    public class DisabilityConfiguration : IEntityTypeConfiguration<Disability>
    {
        public void Configure(EntityTypeBuilder<Disability> builder)
        {
            builder.ToTable("Disability");
            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
