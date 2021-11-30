using Application.Common.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjections
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
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x =>
                    {
                        x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });
                });
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDefaultIdentity<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DomainNameRequirement", policy =>
                {
                    policy.AddRequirements(new WorksForCompanyRequirement("gmail.com"));
                });

                options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator"));
            });

            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();


        }
    }
}
