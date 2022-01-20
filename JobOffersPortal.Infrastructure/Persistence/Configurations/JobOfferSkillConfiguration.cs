using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersPortal.Persistance.EF.Persistence.Configurations
{
    public class JobOfferSkillConfiguration : IEntityTypeConfiguration<JobOfferSkill>
    {
        public void Configure(EntityTypeBuilder<JobOfferSkill> builder)
        {
            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
