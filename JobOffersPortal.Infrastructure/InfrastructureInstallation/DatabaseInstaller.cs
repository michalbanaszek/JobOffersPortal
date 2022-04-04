using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Infrastructure.Security.User;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.Persistance.EF.InfrastructureInstallation
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("MemoryDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DockerConnection"), x =>
                    {
                        x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });
                });
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}