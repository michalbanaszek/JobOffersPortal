using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersPortal.Persistance.EF.Persistence.Configurations
{
    public class JobOfferPropositionConfiguration : IEntityTypeConfiguration<JobOfferProposition>
    {
        public void Configure(EntityTypeBuilder<JobOfferProposition> builder)
        {
            builder.Property(jp => jp.Id).ValueGeneratedOnAdd();

            builder.Property(jp => jp.Content).IsRequired().HasMaxLength(50);

            builder.HasOne(jp => jp.JobOffer).WithMany(j => j.Propositions).HasForeignKey(jp => jp.JobOfferId);
        }
    }
}
