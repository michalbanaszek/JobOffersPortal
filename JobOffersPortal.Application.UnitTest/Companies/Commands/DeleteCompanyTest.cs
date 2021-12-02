using JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public class DeleteCompanyTest : BaseCompanyInitialization
    {
        [Fact]
        public async Task Handle_ValidCompany_AddedToCompanyRepo()
        {
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, null, null);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new DeleteCompanyCommand()
            {
                 Id = "1"
            };

            await handler.Handle(command, CancellationToken.None);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();
           
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount - 1);
        }
    }
}
