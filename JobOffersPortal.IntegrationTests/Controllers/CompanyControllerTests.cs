﻿using FluentAssertions;
using JobOffersPortal.API;
using JobOffersPortal.API.Services;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.IntegrationTests.Fakes;
using JobOffersPortal.Persistance.EF.Persistence;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.IntegrationTests.Controllers
{
    public class CompanyControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private const string JSON_CONTENT_TYPE = "application/json";

        public CompanyControllerTests(WebApplicationFactory<Startup> factory)
        {
            var application = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextOptions);

                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.AddMvc(option => option.Filters.Add(new FakeUserFilter()));

                    services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDb"));
                });
            });

            _client = application.CreateClient();
        }

        [Theory]
        [InlineData("pageNumber=1&pageSize=5")]
        [InlineData("pageNumber=2&pageSize=10")]
        [InlineData("pageNumber=3&pageSize=15")]
        public async Task GetAll_WithQueryParameters_ReturnsOkStatus(string queryParams)
        {
            //Act
            var response = await _client.GetAsync(ApiRoutes.CompanyRoute.GetAll + "?" + queryParams);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("pageNumber=1&pageSize=101")]
        [InlineData("pageNumber=3&pageSize=170")]
        public async Task GetAll_WithInvalidQueryParameters_ReturnsBadRequestStatus(string queryParams)
        {
            //Act
            var response = await _client.GetAsync(ApiRoutes.CompanyRoute.GetAll + "?" + queryParams);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_ValidId_ReturnsOkStatus()
        {
            //Arrange
            string id = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Company1" });

            //Act
            var response = await _client.GetAsync(ApiRoutes.CompanyRoute.Get.Replace("{id}", id));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void Get_InvalidId_ReturnsNotFoundStatus()
        {
            //Arrange
            string id = "99";

            //Act
            var response = await _client.GetAsync(ApiRoutes.CompanyRoute.Get.Replace("{id}", id));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsCreatedStatus()
        {
            //Arrange
            var command = new CreateCompanyCommand()
            {
                Name = "NewCompany5"
            };

            var json = JsonConvert.SerializeObject(command);

            var httpContent = new StringContent(json, Encoding.UTF8, JSON_CONTENT_TYPE);

            //Act
            var response = await _client.PostAsync(ApiRoutes.CompanyRoute.Create, httpContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_InvalidModel_ReturnsBadRequestStatus()
        {
            //Arrange
            var command = new CreateCompanyCommand() { Name = "NewCompany/" };

            var json = JsonConvert.SerializeObject(command);

            var httpContent = new StringContent(json, Encoding.UTF8, JSON_CONTENT_TYPE);

            //Act
            var response = await _client.PostAsync(ApiRoutes.CompanyRoute.Create, httpContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Update_ValidModel_ReturnsOkStatus()
        {
            //Arrange
            string id = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Company1" });

            var command = new UpdateCompanyCommand() { Id = id, Name = "UpdateCompany1" };

            var json = JsonConvert.SerializeObject(command);

            var httpContent = new StringContent(json, Encoding.UTF8, JSON_CONTENT_TYPE);

            //Act
            var response = await _client.PutAsync(ApiRoutes.CompanyRoute.Update.Replace("{id}", command.Id), httpContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Update_InvalidId_ReturnsNotFoundStatus()
        {
            //Assert
            var command = new UpdateCompanyCommand { Id = "99", Name = "UpdateCompany4" };

            var json = JsonConvert.SerializeObject(command);

            var httpContent = new StringContent(json, Encoding.UTF8, JSON_CONTENT_TYPE);

            //Act
            var response = await _client.PutAsync(ApiRoutes.CompanyRoute.Update.Replace("{id}", "99"), httpContent);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsNoContentStatus()
        {
            //Assert
            string id = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Company1"});

            //Act
            var response = await _client.DeleteAsync(ApiRoutes.CompanyRoute.Delete.Replace("{id}", id));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnsNotFoundStatus()
        {
            //Assert
            string id = "99";

            //Act
            var response = await _client.DeleteAsync(ApiRoutes.CompanyRoute.Delete.Replace("{id}", id));

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private async Task<string> CreateCompanyAsync(CreateCompanyCommand request)
        {
            var response = await _client.PostAsJsonAsync(ApiRoutes.CompanyRoute.Create, request);
            var location = response.Headers.Location.ToString();
            var splitLocation = location.Split('/');
            string id = splitLocation[5];
            return id;
        }
    }
}
