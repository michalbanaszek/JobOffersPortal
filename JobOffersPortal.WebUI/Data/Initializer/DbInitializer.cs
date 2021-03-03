using Bogus;
using JobOffersPortal.WebUI.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JobOffersPortal.WebUI.Data.Initializer
{
    public class DbInitializer
    {
        public static async Task Initialize(IServiceScope service)
        {
            DataContext context = service.ServiceProvider.GetRequiredService<DataContext>();

            context.Database.EnsureCreated();

            if (context.Companies.Any())
            {
                return;
            }

            var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = service.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            await CreateRoles(context, roleManager, userManager);

            await SeedDatabase(context, userManager);
        }

        private static async Task CreateRoles(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
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

        private static async Task SeedDatabase(DataContext context, UserManager<IdentityUser> userManager)
        {
            var positions = new[] { "Tlumacz angielskiego", "Pakowacz", "Slusarz", "Murarz" };

            var skills = new[] 
            { 
                "znajomość rysunku technicznego, umiejętność czytania dokumentacji technicznej,posługiwanie się urządzeniami pomiarowymi,doskonała organizacja pracy,umiejętność pracy w zespole",
                "znajomość rysunku technicznego,umiejętność pracy na komputerze,umiejętność czytania schematów elektrycznych",
                "dobra znajomość rysunku technicznego,umiejętność posługiwania się narzędziami pomiarowymi (suwmiarka, mikrometr, średnicówka),znajomość oprogramowania HAAS lub FANUC"
            };

            var requirements = new[] 
            { 
                "bardzo dobra znajomość języka niemieckiego w mowie i piśmie, mile widziane doświadczenie w zarządzaniu projektami, kreatywność oraz dobra organizacja pracy",
                "wykształcenie min. zawodowe, doświadczenie zawodowe na wyżej wymienionym stanowisku" 
            };

            var offers = new[] 
            {
                "Umowę o pracę na pełen etat,Odpowiednie do stanowiska narzędzia pracy,Atrakcyjne wynagrodzenie,Terminowość w wypłacaniu wynagrodzenia,Dobre warunki pracy,Możliwość rozwoju zawodowego",
                "Stałą pracę w firmie o uregulowanej pozycji rynkowej,Odpowiednie do stanowiska narzędzia pracy,Atrakcyjne wynagrodzenie" 
            };

            var salaries = new[] { 2000, 2500, 3000, 3200, 4000, 5000 };

            var user1 = new IdentityUser() { Email = "user@gmail.com", UserName = "user" };
            var user2 = new IdentityUser() { Email = "user2@gmail.com", UserName = "user2" };
            string userPassword = "Qwerty!1";

            var users = new List<IdentityUser>()
            {
                user1, user2
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, userPassword);
            }

            List<JobOffer> jobOffers = new List<JobOffer>()
            {
             new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Offers = offers[0],
                Requirements = requirements[0],
                Salary = salaries[0],
                Skills = skills[0],
                Position = positions[0],
                UserId = user1.Id
            },
              new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Offers = offers[1],
                Requirements = requirements[1],
                Salary = salaries[1],
                Skills = skills[1],
                Position = positions[1],
                UserId = user1.Id
            },
              new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                Date = DateTime.Now,
                Offers = offers[1],
                Requirements = requirements[1],
                Salary = salaries[2],
                Skills = skills[2],
                Position = positions[2],
                UserId = user2.Id
            }
            };

            var companyJobOffer1 = new List<CompanyJobOffer>()
            {
                 new CompanyJobOffer() { JobOfferId = jobOffers[0].Id },
                 new CompanyJobOffer() { JobOfferId = jobOffers[1].Id }
            };

            var companyJobOffer2 = new List<CompanyJobOffer>()
            {
                 new CompanyJobOffer() { JobOfferId = jobOffers[2].Id }
            };

            var companies = new List<Company>()
            {
                new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Company1",
                UserId = user1.Id,
                JobOffers = companyJobOffer1
            },
                new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Company2",
                UserId = user2.Id,
                JobOffers = companyJobOffer2
            }
            };

            context.JobOffers.AddRange(jobOffers);            
            context.Companies.AddRange(companies);

            context.SaveChanges();
        }
    }
}
