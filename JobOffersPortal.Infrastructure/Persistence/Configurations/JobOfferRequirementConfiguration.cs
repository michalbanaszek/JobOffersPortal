using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersPortal.Persistance.EF.Persistence.Configurations
{
    public class JobOfferRequirementConfiguration : IEntityTypeConfiguration<JobOfferRequirement>
    {
        public void Configure(EntityTypeBuilder<JobOfferRequirement> builder)
        {
            builder.Property(jr => jr.Id).ValueGeneratedOnAdd();

            builder.Property(jr => jr.Content).IsRequired().HasMaxLength(50);

            builder.HasOne(jr => jr.JobOffer).WithMany(j => j.Requirements).HasForeignKey(jr => jr.JobOfferId);
        }
    }
}
