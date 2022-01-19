using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Infrastructure.Security.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace JobOffersPortal.Persistance.EF.Persistence
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
                "TŁUMACZ NIEMIECKIEGO",
                "Pakowacz",
                "Slusarz",
                "Murarz",
                "MAGAZYNIER BLACH",
                "MAGAZYNIER"
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
                "doświadczenie zawodowe na wyżej wymienionym stanowisku",
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

            var skills2 = new[]
         {
                "przyjmowanie towarów do magazynu",
                "uczestnictwo w odbiorze ilościowym i jakościowym",
                "sporządzanie odpowiedniej dokumentacji",
                "prowadzenie dokumentacji magazynowej przy użyciu programów komputerowych lub metodami tradycyjnymi"
            };

            var requirements2 = new[]
          {
                "konieczne uprawnienia na wózki z doświadczeniem jazdy",
                "podstawowa znajomość obiegu dokumentacji magazynowej"
            };

            var skills3 = new[]
    {
                "Obsługa komputera i programów magazynowych",
                "Przyjęcie dostaw stali do magazynu",
                "Sprawdzanie zgodności dokumentów dostawy z dostarczonym towarem",
                "Wydawanie materiału na produkcje",
                "Stała kontrola stanów magazynowych",
                "Przestrzeganie zasad obiegu dokumentów",
                "Dbałość o materiały, o czystość oraz higienę w magazynie"
            };

            var requirements3 = new[]
          {
                "Wykształcenie minimum zawodowe",
                "Uprawnienia na wózki widłowe",
                "Znajomość podstawowych materiałów stalowych takich jak profile, blachy, pręty",
                "Dobra kondycja fizyczna",
                "Dyspozycyjność, dokładność i odpowiedzialność",
                "Umiejętność pracy w zespole, chęć i gotowość do intensywnej pracy",
                "Precyzja i rzetelność w wykonywaniu powierzonych zadań"
            };

            var propositions3 = new[]
            {
                "Stałą pracę w firmie o uregulowanej pozycji rynkowej",
                "Odpowiednie do stanowiska narzędzia pracy",
                "Atrakcyjne wynagrodzenie",
                "Stałość w terminach wypłat",
                "Dobre warunki pracy",
                "Możliwość rozwoju zawodowego"
            };

            var salaries = new[] { "2000", "2500", "3000", "3200", "4000", "5000" };

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
                Id = "0f2de8b6-9160-4843-b36e-90372a3f8179",
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
                IsAvailable = true,
                Propositions = new List<JobOfferProposition>()
                {
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[0] },
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[1] },
                      new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[2] },
                      new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[3] },
                        new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[4] },
                          new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[5] },
                },
                Requirements = new List<JobOfferRequirement>()
                {
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[0] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[1] },
                         new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[2] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[3] },
                         new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[4] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[5] },
                     new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[6] },
                },
                Skills = new List<JobOfferSkill>()
                {
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[0] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[1] },
                       new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[2] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[3] },
                       new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[4] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[5] },
                       new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[6] }

                },
                Salary = salaries[0],
                Position = positions[4],
                CreatedBy = user1.Id,
                Created = DateTime.Now
            },
             new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companies[0].Id,
                Date = DateTime.Now,
                IsAvailable = true,
                    Propositions = new List<JobOfferProposition>()
                {
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[0] },
                    new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = propositions3[1] },
                },
                Requirements = new List<JobOfferRequirement>()
                {
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[0] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements3[1] },
                },
                Skills = new List<JobOfferSkill>()
                {
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[0] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills3[1] },
                },
                Salary = salaries[0],
                Position = positions[5],
                CreatedBy = user1.Id,
                Created = DateTime.Now
            },
             new JobOffer()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = companies[0].Id,
                Date = DateTime.Now,
                IsAvailable = true,
                Requirements = new List<JobOfferRequirement>()
                {
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements2[0] },
                    new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = requirements2[1] },
                },
                Skills = new List<JobOfferSkill>()
                {
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills2[0] },
                    new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = skills2[1] },
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
                IsAvailable = true,
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
                IsAvailable = true,
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
