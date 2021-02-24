using JobOffersPortal.Contracts.Contracts.Responses;
using JobOffersPortal.WebUI;
using JobOffersPortal.WebUI.Contracts.Requests;
using JobOffersPortal.WebUI.Contracts.Responses;
using JobOffersPortal.WebUI.Data;
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
        private object _lockObj = new object();

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DataContext>));

                        if (descriptor != null)
                        {
                            services.Remove(descriptor);
                        }

                        services.AddDbContext<DataContext>(options =>
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

        protected async Task<Response<CompanyResponse>> CreateCompanyAsync(CreateCompanyRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Company.Create, request);

            return await response.Content.ReadAsAsync<Response<CompanyResponse>>();
        }


        private async Task<string> GetJwtAsync()
        {
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new RegisterRequest()
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
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context.Database.EnsureDeleted();
        }
    }
}
