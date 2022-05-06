using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure.Persistence.Configurations
{
    public class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.Property(j => j.Id).ValueGeneratedOnAdd();

            builder.Property(j => j.Position).IsRequired().HasMaxLength(30);

            builder.Property(j => j.Salary).HasMaxLength(30);

            builder.Property(j => j.CompanyId).IsRequired();

            builder.Property(j => j.Date).HasDefaultValue(DateTime.Now).IsRequired();

            builder.HasOne(j => j.Company).WithMany(c => c.JobOffers).HasForeignKey(j => j.CompanyId);
        }
    }
}
