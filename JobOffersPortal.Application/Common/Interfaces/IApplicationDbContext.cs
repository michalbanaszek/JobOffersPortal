using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Company> Companies { get; set; }
        DbSet<JobOffer> JobOffers { get; set; }
        DbSet<JobOfferProposition> JobOfferPropositions { get; set; }
        DbSet<JobOfferRequirement> JobOfferRequirements { get; set; }
        DbSet<JobOfferSkill> JobOfferSkills { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
