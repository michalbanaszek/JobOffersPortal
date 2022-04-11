using AutoMapper;
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
    public class CreateCompanyHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<ILogger<CreateCompanyCommandHandler>> _logger;
        private readonly CreateCompanyCommandValidator _validator;
        private readonly IMapper _mapper;

        public CreateCompanyHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockUriService = MockUriService.GetUriService();
            _logger = new Mock<ILogger<CreateCompanyCommandHandler>>();
            _validator = new CreateCompanyCommandValidator(_mockCompanyRepository.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCompany_AddedToCompanyRepo()
        {
            //Arrange
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockUriService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = "New Company",
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await handler.Handle(command, CancellationToken.None);

            //Assert
            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount + 1);

            validatorResult.IsValid.ShouldBeTrue();

            response.Uri.ShouldNotBeNull();
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyCompany_NotAddedToCompanyRepo()
        {
            //Arrange
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockUriService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = string.Empty
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company cannot be empty.");

            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Name' must be at least 2 characters. You entered 0 characters.");

            allCompanies.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthCompany_NotAddedToCompanyRepo()
        {
            //Arrange
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockUriService.Object);

            var command = new CreateCompanyCommand() { Name = new string('a', 31), };

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company Length is beewten 2 and 30");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatCompany_NotAddedToCompanyRepo()
        {
            //Arrange
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockUriService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = new string('*', 10),
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Name' is not in the correct format.");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_IsAlreadyNameExistCompany_NotAddedToCompanyRepo()
        {
            //Arrange
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockUriService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = "CompanyName1",
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company with the same Name already exist.");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }
    }
}