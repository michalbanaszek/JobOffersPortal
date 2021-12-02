using JobOffersPortal.API;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Models.Requests;
using JobOffersPortal.Application.Common.Models.Responses;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace JobOffersPortal.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;       

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<ApplicationDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDbInMemory");
                        });
                    });
                });

            _serviceProvider = appFactory.Services;
            _httpClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }

        protected async Task<string> CreateCompanyAsync(CreateCompanyCommand request)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.CompanyRoute.Create, request);

            return await response.Content.ReadAsAsync<string>();
        }


        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.IdentityRoute.Register, new RegisterRequest()
            {
                Email = "test123@gmail.com",
                Password = "Qwerty!1"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();


            return registrationResponse.Token;
        }

        public void Dispose()
        {            
                using var serviceScope = _serviceProvider.CreateScope();
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureDeleted();
        }
    }
}
