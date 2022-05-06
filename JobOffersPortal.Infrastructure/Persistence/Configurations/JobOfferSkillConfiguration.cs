using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobOffersPortal.Persistance.EF.Persistence.Configurations
{
    public class JobOfferSkillConfiguration : IEntityTypeConfiguration<JobOfferSkill>
    {
        public void Configure(EntityTypeBuilder<JobOfferSkill> builder)
        {
            builder.Property(js => js.Id).ValueGeneratedOnAdd();

            builder.Property(js => js.Content).IsRequired().HasMaxLength(50);

            builder.HasOne(js => js.JobOffer).WithMany(j => j.Skills).HasForeignKey(js => js.JobOfferId);
        }
    }
}
