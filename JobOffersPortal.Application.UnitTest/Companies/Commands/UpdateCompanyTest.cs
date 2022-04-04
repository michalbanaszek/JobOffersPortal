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
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "UpdateCompany" };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var entityUpdated = await _mockCompanyRepository.Object.GetByIdAsync(command.Id);

            entityUpdated.Id.ShouldBe("1");

            entityUpdated.Name.ShouldBe("UpdateCompany");
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyCompany_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new UpdateCompanyCommand() { Name = string.Empty };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company cannot be empty.");

            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Name' must be at least 2 characters. You entered 0 characters.");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthCompany_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = new string('a', 31) };

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

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company Length is between 2 and 30");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatCompany_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = new string('*', 5) };          

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Name' is not in the correct format.");
        }

        [Fact]
        public async Task HandleValidator_IsAlreadyNameExistCompany_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "CompanyName1" };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Company with the same Name already exist.");
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidCompanyId_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "99", Name = "UpdateCompanyName" };

            NotFoundException exceptionResponse = null;

            //Act
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException exception)
            {
                exceptionResponse = exception;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"Company\" (99) was not found.");
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotUpdatedToCompanyRepo()
        {
            //Arrange
            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user2");

            ForbiddenAccessException exceptionResponse = null;

            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "UpdateCompanyName" };

            //Act
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (ForbiddenAccessException exception)
            {
                exceptionResponse = exception;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"Company\" (1) do not own this entity.");
        }
    }
}