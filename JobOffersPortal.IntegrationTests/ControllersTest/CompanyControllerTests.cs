using Application;
using Application.Companies.Commands.CreateCompany;
using Application.Companies.Commands.UpdateCompany;
using Application.Companies.Queries.GetCompanies;
using Application.Response;
using FluentAssertions;
using JobOffersPortal.WebUI;
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
            var response = await _httpClient.GetAsync(ApiRoutes.Company.GetAll);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<PagedResponse<CompanyDto>>()).Data.Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsCompany_WhenCompanyExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdCompany = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Test Company" });

            // Act
            var response = await _httpClient.GetAsync(ApiRoutes.Company.Get.Replace("{id}", createdCompany.Data.Id));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedCompany = await response.Content.ReadAsAsync<Response<CompanyResponse>>();
            returnedCompany.Data.Id.Should().Be(createdCompany.Data.Id);
            returnedCompany.Data.Name.Should().Be("Test Company");
        }

        [Fact]
        public async Task Update_ReturnsUpdatedCompany_WhenCompanyExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdCompany = await CreateCompanyAsync(new CreateCompanyCommand() { Name = "Test Company" });
            var updatedCompany = new UpdateCompanyCommand()
            {
                Name = "Updated Company"
            };

            // Act
            var response = await _httpClient.PutAsJsonAsync(ApiRoutes.Company.Update.Replace("{id}", createdCompany.Data.Id), updatedCompany);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedCompany = await response.Content.ReadAsAsync<Response<CompanyResponse>>();
            returnedCompany.Data.Id.Should().Be(createdCompany.Data.Id);
            returnedCompany.Data.Name.Should().Be(updatedCompany.Name);
        }

        [Fact]
        public async Task Create_ReturnsCreatedCompany()
        {
            // Arrange
            await AuthenticateAsync();
            var createdCompany = new CreateCompanyCommand() { Name = "Test Company" };
         
            // Act
            var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Company.Create, createdCompany);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var returnedCompany = await response.Content.ReadAsAsync<Response<CompanyResponse>>();
            returnedCompany.Data.Name.Should().Be(createdCompany.Name);
        }

        [Fact]
        public async Task Delete_ReturnsEmptyResponse_WhenCompanyExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdCompanyRequest = new CreateCompanyCommand() { Name = "Test Company" };
            var createdCompanyResponse = await CreateCompanyAsync(createdCompanyRequest);

            // Act
            var response = await _httpClient.DeleteAsync(ApiRoutes.Company.Delete.Replace("{id}", createdCompanyResponse.Data.Id));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);            
        }
    }
}
