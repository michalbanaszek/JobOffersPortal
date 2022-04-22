using JobOffersPortal.Domain.Entities;
using JobOffersPortal.Infrastructure.Security.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Persistance.EF.Persistence
{
    public class ApplicationDbContextSeed
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationDbContextSeed(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (_context.Database.IsRelational())
                {
                    var pendingMigrations = _context.Database.GetPendingMigrations();

                    if (pendingMigrations != null && pendingMigrations.Any())
                    {
                        _context.Database.Migrate();
                    }
                }

                if (!_context.Roles.Any())
                {
                    SeedRoles(_roleManager);
                }

                if (!_context.Users.Any())
                {
                    SeedUsers(_userManager);
                }

                if (!_context.Companies.Any())
                {
                    SeedData(_context);
                }
            }
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Id = "1";
                user.UserName = "admin";
                user.Email = "admin@localhost";
                user.EmailConfirmed = true;

                IdentityResult userResult = userManager.CreateAsync(user, "Qwerty!1").Result;

                if (userResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }

            if (userManager.FindByNameAsync("user1").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Id = "2";
                user.UserName = "user1";
                user.Email = "user1@localhost";
                user.EmailConfirmed = true;

                IdentityResult userResult = userManager.CreateAsync(user, "Qwerty!1").Result;

                if (userResult.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Customer").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!(roleManager.RoleExistsAsync("Customer").Result))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!(roleManager.RoleExistsAsync("Administrator").Result))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        private static void SeedData(ApplicationDbContext context)
        {
            var positions = new[]
            {
                "Tłumacz niemieckiego",
                "Pakowacz",
                "Slusarz",
                "Murarz",
                "Magazynier blach",
                "Magazynier"
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

            var companies = new List<Company>()
            {
                new Company()
                {
                    Id = "1",
                    Name = "Company1",
                    CreatedBy = "1",
                    Created = DateTime.Now
                },
                new Company()
                {
                    Id = "2",
                    Name = "Company2",
                    CreatedBy = "2",
                    Created = DateTime.Now
                },
                new Company()
                {
                    Id = "3",
                    Name = "Company3",
                    CreatedBy = "1",
                    Created = DateTime.Now
                },
                   new Company()
                {
                    Id = "4",
                    Name = "Company4",
                    CreatedBy = "2",
                    Created = DateTime.Now
                }
            };

            List<JobOffer> jobOffers = new List<JobOffer>()
            {
                new JobOffer()
                {
                    Id = "1",
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
                    CreatedBy = "1",
                    Created = DateTime.Now
                },
                 new JobOffer()
                 {
                    Id = "2",
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
                    CreatedBy = "1",
                    Created = DateTime.Now
                 },
                 new JobOffer()
                 {
                    Id = "3",
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
                    CreatedBy = "1",
                    Created = DateTime.Now
                 },
                 new JobOffer()
                 {
                    Id = "4",
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
                    CreatedBy = "1",
                    Created = DateTime.Now
                 },
                 new JobOffer()
                 {
                    Id = "5",
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
                    CreatedBy = "2",
                    Created = DateTime.Now
                 }
            };

            context.JobOffers.AddRange(jobOffers);
            context.Companies.AddRange(companies);
            context.SaveChanges();
        }
    }
}