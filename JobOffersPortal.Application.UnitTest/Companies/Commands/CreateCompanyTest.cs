using FluentValidation.Results;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public class CreateCompanyTest : BaseCompanyInitialization
    {
        private readonly ILogger<CreateCompanyCommandHandler> _logger;
        private readonly IUriCompanyService _uriCompanyService;
        private readonly CreateCompanyCommandValidator _validator;

        public CreateCompanyTest()
        {
            _logger = (new Mock<ILogger<CreateCompanyCommandHandler>>()).Object;
            _uriCompanyService = (new Mock<IUriCompanyService>()).Object;  
            _validator = new CreateCompanyCommandValidator(_mockCompanyRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCompany_AddedToCompanyRepo()
        {
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _uriCompanyService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = "New Company",
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.Succeeded.ShouldBe(true);
            response.Errors.Length.ShouldBe(0);
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount + 1);
            response.Id.ShouldNotBeNull();
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyCompany_NotAddedToCompanyRepo()
        {
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _uriCompanyService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = string.Empty
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company cannot be empty.");
            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Name' must be at least 2 characters. You entered 0 characters.");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthCompany_NotAddedToCompanyRepo()
        {
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _uriCompanyService);

            var command = new CreateCompanyCommand() { Name = new string('a', 31), };

            var validatorResult = await _validator.ValidateAsync(command);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company Length is beewten 2 and 30");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatCompany_NotAddedToCompanyRepo()
        {
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _uriCompanyService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = new string('*', 10),
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Name' is not in the correct format.");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_IsAlreadyNameExistCompany_NotAddedToCompanyRepo()
        {
            var handler = new CreateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _uriCompanyService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new CreateCompanyCommand()
            {
                Name = "CompanyName1",
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company with the same Name already exist.");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        private static async Task<CreateCompanyCommandResponse> CheckValidationResult(CreateCompanyCommandHandler handler, CreateCompanyCommand command, ValidationResult validatorResult)
        {
            CreateCompanyCommandResponse createCompanyResponse = null;

            if (validatorResult.IsValid)
            {
                createCompanyResponse = await handler.Handle(command, CancellationToken.None);
            }

            return createCompanyResponse;
        }
    }
}