using JobOffersPortal.WebUI.Data;
using JobOffersPortal.WebUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JobOffersPortal.WebUI.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<DataContext>();

            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobOfferService, JobOfferService>();
        }
    }
}
