using Application.Companies.Queries.GetCompanies;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task Initialize(IServiceScope service)
        {
            ApplicationDbContext context = service.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (context.Companies.Any())
            {
                return;
            }

            var roleManager = service.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManager = service.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await CreateRoles(roleManager, userManager);

            await SeedDatabase(context, userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
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

            var adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", EmailConfirmed = true };
            var customerUser = new ApplicationUser { UserName = "customer@gmail.com", Email = "customer@gmail.com", EmailConfirmed = true };

            string adminPassword = "Qwerty!1";

            var adminUsers = new List<ApplicationUser>()
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

        private static async Task SeedDatabase(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var positions = new[]
            {
                "Tlumacz angielskiego",
                "Pakowacz",
                "Slusarz",
                "Murarz"
            };

            var skills = new[]
            {
                "znajomość rysunku technicznego",
                "umiejętność czytania dokumentacji technicznej",
                "posługiwanie się urządzeniami pomiarowymi",
                "doskonała organizacja pracy",
                "umiejętność pracy w zespole",
                "znajomość rysunku technicznego",
                "umiejętność pracy na komputerze",
                "umiejętność czytania schematów elektrycznych",
                "umiejętność posługiwania się narzędziami pomiarowymi (suwmiarka, mikrometr, średnicówka)",
                "znajomość oprogramowania HAAS"
            };

            var requirements = new[]
            {
                "bardzo dobra znajomość języka niemieckiego w mowie i piśmie",
                "mile widziane doświadczenie w zarządzaniu projektami",
                "kreatywność oraz dobra organizacja pracy",
                "wykształcenie min. zawodowe",
                "doświadczenie zawodowe na wyżej wymienionym stanowisku"
            };

            var propositions = new[]
            {
                "umowę o pracę na pełen etat",
                "odpowiednie do stanowiska narzędzia pracy",
                "atrakcyjne wynagrodzenie",
                "terminowość w wypłacaniu wynagrodzenia",
                "dobre warunki pracy",
                "możliwość rozwoju zawodowego",
                "stałą pracę w firmie o uregulowanej pozycji rynkowej"
            };

            var salaries = new[] { 2000, 2500, 3000, 3200, 4000, 5000 };

            var user1 = new ApplicationUser() { Email = "user1@gmail.com", UserName = "user1@gmail.com" };
            var user2 = new ApplicationUser() { Email = "user2@gmail.com", UserName = "user2@gmail.com" };

            string userPassword = "Qwerty!1";

            var users = new List<ApplicationUser>()
            {
                user1, user2
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, userPassword);
            }

            var companies = new List<Company>()
            {
                new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Company1",
                CreatedBy = user1.Id,
                Created = DateTime.Now

            },
                new Company()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Company2",
                CreatedBy = user2.Id,
                Created = DateTime.Now
            }
            };

            List<JobOffer> jobOffers = new List<JobOffer>()
            {
             new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companies[0].Id,
                Date = DateTime.Now,
                Propositions = new List<JobOfferProposition>()
                {
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions[0] },
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions[1] },
                },
                Requirements = new List<JobOfferRequirement>()
                {
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements[0] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements[1] },
                },
                Skills = new List<JobOfferSkill>()
                {
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills[0] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills[1] },
                },
                Salary = salaries[0],
                Position = positions[0],
                CreatedBy = user1.Id,
                Created = DateTime.Now
            },
              new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companies[1].Id,
                Date = DateTime.Now,
                 Propositions = new List<JobOfferProposition>()
                {
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions[2] },
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions[3] },
                },
                Requirements = new List<JobOfferRequirement>()
                {
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements[2] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements[3] },
                },
                Skills = new List<JobOfferSkill>()
                {
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills[2] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills[3] },
                },
                Salary = salaries[1],
                Position = positions[1],
                CreatedBy = user1.Id,
                Created = DateTime.Now
            },
              new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companies[1].Id,
                Date = DateTime.Now,
                 Propositions = new List<JobOfferProposition>()
                {
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions[4] },
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions[5] },
                },
                Requirements = new List<JobOfferRequirement>()
                {
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements[3] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements[4] },
                },
                Skills = new List<JobOfferSkill>()
                {
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills[4] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills[5] },
                },
                Salary = salaries[2],
                Position = positions[2],
                CreatedBy = user2.Id,
                Created = DateTime.Now
            }
            };

            context.JobOffers.AddRange(jobOffers);
            context.Companies.AddRange(companies);
            await context.SaveChangesAsync();
        }
    }
}
