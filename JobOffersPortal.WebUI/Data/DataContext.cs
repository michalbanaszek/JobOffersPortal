using JobOffersPortal.WebUI.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobOffersPortal.WebUI.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {            
        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<CompanyJobOffer> CompanyJobOffers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
        //    {
        //        switch (entry.State)
        //        {                  
        //            case EntityState.Modified:
        //                entry.Entity.LastModifiedBy = "System";
        //                entry.Entity.LastModified = DateTime.Now;
        //                break;
        //            case EntityState.Added:
        //                entry.Entity.CreatedBy = "System";
        //                entry.Entity.Created = DateTime.Now;
        //                entry.Entity.LastModifiedBy = "System";
        //                entry.Entity.LastModified = DateTime.Now;
        //                break;
        //            default:
        //                break;
        //        }
        //    }

        //    int result = await base.SaveChangesAsync(cancellationToken);

        //    return result;
        //}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Company>()
                   .Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Entity<JobOffer>()
                  .Property(p => p.Id)
                  .ValueGeneratedOnAdd();

            builder.Entity<CompanyJobOffer>()
                   .Ignore(xx => xx.Company).HasKey(x => new { x.CompanyId, x.JobOfferId });

            builder.Entity<RefreshToken>()
                  .Property(p => p.Token)
                  .ValueGeneratedOnAdd();
        }
    }
}
