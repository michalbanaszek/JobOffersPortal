using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersPortal.Persistance.EF.Persistence.Configurations
{
    public class JobOfferRequirementConfiguration : IEntityTypeConfiguration<JobOfferRequirement>
    {
        public void Configure(EntityTypeBuilder<JobOfferRequirement> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired();
        }
    }
}
