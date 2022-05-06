using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Token);

            builder.Property(r => r.Token).ValueGeneratedOnAdd();

            builder.Property(r => r.JwtId).IsRequired();

            builder.Property(r => r.CreationDate).IsRequired();

            builder.Property(r => r.Used).IsRequired();

            builder.Property(r => r.ExpiryDate).IsRequired();
        }
    }
}