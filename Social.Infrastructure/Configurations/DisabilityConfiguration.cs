﻿using Microsoft.EntityFrameworkCore;
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
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}