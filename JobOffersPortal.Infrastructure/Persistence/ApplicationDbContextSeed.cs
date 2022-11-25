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

            var skills1 = new[]
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

            var requirements1 = new[]
       {
                "bardzo dobra znajomość języka niemieckiego w mowie i piśmie",
                "mile widziane doświadczenie w zarządzaniu projektami",
                "kreatywność oraz dobra organizacja pracy",
                "wykształcenie min. zawodowe",
                "doświadczenie zawodowe na wyżej wymienionym stanowisku",
            };

            var propositions1 = new[]
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

            var propositions2 = new[]
            {
                "Stałą pracę w firmie o uregulowanej pozycji rynkowej",
                "Odpowiednie do stanowiska narzędzia pracy",
                "Atrakcyjne wynagrodzenie",
                "terminowość w wypłacaniu wynagrodzenia",
                "Dobre warunki pracy",
                "Możliwość rozwoju zawodowego"
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

            var companies = new List<Company>();

            var company1 = new Company("1", "Company1") { CreatedBy = "1", Created = DateTime.Now };

            var jobOffer1 = company1.AddJobOffer(companies[0].Id, positions[0], "1000", DateTime.Now, true);
            jobOffer1.AddProposition(propositions1[0], "1");
            jobOffer1.AddProposition(propositions1[1], "1");
            jobOffer1.AddProposition(propositions1[2], "1");

            jobOffer1.AddRequirement(requirements1[0], "1");
            jobOffer1.AddRequirement(requirements1[1], "1");
            jobOffer1.AddRequirement(requirements1[2], "1");

            jobOffer1.AddSkill(skills1[0], "1");
            jobOffer1.AddSkill(skills1[1], "1");
            jobOffer1.AddSkill(skills1[2], "1");

            var company2 = new Company("2", "Company2") { CreatedBy = "2", Created = DateTime.Now };

            var jobOffer2 = company2.AddJobOffer(companies[1].Id, positions[1], "2000", DateTime.Now, true);
            jobOffer2.AddProposition(propositions2[0], "2");
            jobOffer2.AddProposition(propositions2[1], "2");
            jobOffer2.AddProposition(propositions2[2], "2");

            jobOffer2.AddRequirement(requirements2[0], "2");
            jobOffer2.AddRequirement(requirements2[1], "2");
            jobOffer2.AddRequirement(requirements2[2], "2");

            jobOffer2.AddSkill(skills2[0], "2");
            jobOffer2.AddSkill(skills2[1], "2");
            jobOffer2.AddSkill(skills2[2], "2");

            companies.Add(company2);

            companies.Add(new Company("2", "Company3") { CreatedBy = "3", Created = DateTime.Now });
            companies.Add(new Company("3", "Company4") { CreatedBy = "4", Created = DateTime.Now });
            companies.Add(new Company("4", "Company5") { CreatedBy = "5", Created = DateTime.Now });

            context.Companies.AddRange(companies);
            context.SaveChanges();
        }
    }
}