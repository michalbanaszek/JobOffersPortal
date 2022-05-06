using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            
            builder.Property(c => c.Name).IsRequired().HasMaxLength(30);

            builder.HasMany<JobOffer>(c => c.JobOffers).WithOne(j => j.Company).HasForeignKey(j => j.CompanyId);
        }
    }
}
