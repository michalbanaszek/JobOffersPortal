using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersPortal.Persistance.EF.Persistence.Configurations
{
    public class JobOfferPropositionConfiguration : IEntityTypeConfiguration<JobOfferProposition>
    {
        public void Configure(EntityTypeBuilder<JobOfferProposition> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired();
        }
    }
}
