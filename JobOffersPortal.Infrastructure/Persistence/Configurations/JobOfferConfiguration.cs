using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class JobOfferConfiguration : IEntityTypeConfiguration<JobOffer>
    {
        public void Configure(EntityTypeBuilder<JobOffer> builder)
        {
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Position)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Salary)
                .HasMaxLength(30);

            builder.Property(x => x.CompanyId)
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();
        }
    }
}
