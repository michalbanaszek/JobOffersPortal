using JobOffersPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobOfferProposition> JobOfferPropositions { get; set; }
        public DbSet<JobOfferRequirement> JobOfferRequirements { get; set; }
        public DbSet<JobOfferSkill> JobOfferSkills { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
