using FluentValidation.Results;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public class UpdateCompanyTest : BaseCompanyInitialization
    {
        private readonly ILogger<UpdateCompanyCommandHandler> _logger;
        private readonly UpdateCompanyCommandValidator _validator;

        public UpdateCompanyTest()
        {
            _logger = (new Mock<ILogger<UpdateCompanyCommandHandler>>()).Object;
            _validator = new UpdateCompanyCommandValidator(_mockCompanyRepository.Object);
        }

        [Fact]
        public async Task Handle_ValidCompany_UpdatedToCompanyRepo()
        {
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "UpdateCompany" };

            var result = await handler.Handle(command, CancellationToken.None);

            var entityUpdated = await _mockCompanyRepository.Object.GetByIdAsync(command.Id);

            result.Succeeded.ShouldBe(true);
            entityUpdated.Id.ShouldBe("1");
            entityUpdated.Name.ShouldBe("UpdateCompany");
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyCompany_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new UpdateCompanyCommand() { Name = string.Empty };

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
        public async Task HandleValidator_InvalidMaxLengthCompany_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = new string('a', 31) };

            var validatorResult = await _validator.ValidateAsync(command);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company Length is between 2 and 30");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatCompany_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = new string('*', 5) };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Name' is not in the correct format.");
        }

        [Fact]
        public async Task HandleValidator_IsAlreadyNameExistCompany_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "CompanyName1" };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company with the same Name already exist.");
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidCompanyId_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "99", Name = "UpdateCompanyName" };

            NotFoundException exceptionResponse = null;

            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException exception)
            {
                exceptionResponse = exception;
            }
            finally
            {
                exceptionResponse.ShouldNotBeNull();
                exceptionResponse.Message.ShouldBe("Entity \"Company\" (99) was not found.");
            }
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotUpdatedToCompanyRepo()
        {
            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user2");

            ForbiddenAccessException exceptionResponse = null;

            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "UpdateCompanyName" };

            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (ForbiddenAccessException exception)
            {
                exceptionResponse = exception;
            }
            finally
            {
                exceptionResponse.ShouldNotBeNull();
                exceptionResponse.Message.ShouldBe("Entity \"Company\" (1) do not own this entity.");
            }
        }

        private static async Task<UpdateCompanyCommandResponse> CheckValidationResult(UpdateCompanyCommandHandler handler, UpdateCompanyCommand command, ValidationResult validatorResult)
        {
            UpdateCompanyCommandResponse updateCompanyResponse = null;

            if (validatorResult.IsValid)
            {
                updateCompanyResponse = await handler.Handle(command, CancellationToken.None);
            }

            return updateCompanyResponse;
        }
    }
}