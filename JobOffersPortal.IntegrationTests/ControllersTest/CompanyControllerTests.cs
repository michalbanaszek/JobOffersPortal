using FluentAssertions;
using JobOffersPortal.Application;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.IntegrationTests.ControllersTest
{
    public class CompanyControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyCompanies_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.CompanyRoute.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            (await response.Content.ReadAsAsync<PaginatedList<GetCompaniesWithJobOffersListWithPaginationQuery>>()).Items.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsCompany_WhenCompanyExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();

            var createdCompany = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Test Company" });

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.CompanyRoute.Get.Replace("{id}", createdCompany));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var returnedCompany = await response.Content.ReadAsAsync<GetCompanyQuery>();

            returnedCompany.Id.Should().Be(createdCompany);
        }

        [Fact]
        public async Task Update_ReturnsUpdatedCompany_WhenCompanyExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();

            var createdCompany = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Test Company" });

            var updatedCompany = new UpdateCompanyCommand()
            {
                Id = createdCompany,
                Name = "Updated Company"
            };

            // Act
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.CompanyRoute.Update.Replace("{id}", createdCompany), updatedCompany);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_ReturnsCreatedCompany()
        {
            // Arrange
            await AuthenticateAsync();

            var createdCompany = new CreateCompanyCommand() { Name = "Test Company" };

            // Act
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.CompanyRoute.Create, createdCompany);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Delete_ReturnsEmptyResponse_WhenCompanyExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();

            var createdCompanyRequest = new CreateCompanyCommand() { Name = "Test Company" };

            var createdCompanyResponse = await CreateCompanyAsync(createdCompanyRequest);

            // Act
            var response = await _httpClient.DeleteAsync(ApiRoutes.CompanyRoute.Delete.Replace("{id}", createdCompanyResponse));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}
