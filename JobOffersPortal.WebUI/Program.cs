using JobOffersPortal.WebUI.Data;
using JobOffersPortal.WebUI.Data.Initializer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobOffersPortal.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

                await dbContext.Database.MigrateAsync();

                await DbInitializer.Initialize(serviceScope);
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });


        private static async Task CreateAdminRole(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            var adminRole = new IdentityRole()
            {
                Name = "Admin"
            };

            var customerRole = new IdentityRole()
            {
                Name = "Customer"
            };

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("Customer"))
            {
                await roleManager.CreateAsync(customerRole);
            }

            var adminUser = new IdentityUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", EmailConfirmed = true };
            var customerUser = new IdentityUser { UserName = "customer@gmail.com", Email = "customer@gmail.com", EmailConfirmed = true };

            string adminPassword = "Qwerty!1";

            var adminUsers = new List<IdentityUser>()
            {
                adminUser, customerUser
            };

            IdentityResult result = null;

            foreach (var admin in adminUsers)
            {
                result = userManager.CreateAsync(admin, adminPassword).Result;
            }

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(customerUser, "Customer");
            }
        }
    }
}
