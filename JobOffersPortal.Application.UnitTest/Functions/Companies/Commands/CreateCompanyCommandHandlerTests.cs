using AutoMapper;
using FluentValidation.TestHelper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Commands
{
    public class CreateCompanyCommandHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<ILogger<CreateCompanyCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public CreateCompanyCommandHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockUriService = MockUriService.GetUriService();
            _logger = new Mock<ILogger<CreateCompanyCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCompany_AddedToCompanyRepository()
        {
            //Arrange
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockUriService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = "New Company",
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            //Assert
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount + 1);

            result.Uri.ShouldNotBeNull();

            result.ShouldBeOfType<CreateCompanyCommandResponse>();
        }
    }
}