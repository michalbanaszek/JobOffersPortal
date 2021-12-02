using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public class CreateCompanyTest : BaseCompanyInitialization
    {
        [Fact]
        public async Task Handle_ValidCompany_AddedToCompanyRepo()
        {
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object,_mapper, null, null);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = "CompanyNameNext"
            };

            var response = await handler.Handle(command, CancellationToken.None);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.Succeeded.ShouldBe(true);
            response.Errors.Length.ShouldBe(0);
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount + 1);
            response.Id.ShouldNotBeNull();
        }
    }
}
