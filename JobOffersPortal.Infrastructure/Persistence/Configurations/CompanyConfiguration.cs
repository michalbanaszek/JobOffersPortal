﻿using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(30);
        }
    }
}
