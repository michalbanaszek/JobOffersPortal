using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Commands
{
    public class UpdateCompanyHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<UpdateCompanyCommandHandler>> _logger;
        private readonly UpdateCompanyCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateCompanyHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<UpdateCompanyCommandHandler>>();
            _validator = new UpdateCompanyCommandValidator(_mockCompanyRepository.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCompany_UpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

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
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

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
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

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
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

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
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

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
        public void Handle_ForInvalidCompanyId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateCompanyCommand() { Id = "99", Name = "UpdateCompanyName" };

            //Act        
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(func);
        }

        [Theory]
        [InlineData("1", "2")]
        [InlineData("2", "1")]
        public void Handle_ForNotOwnerUserForCompanyId_ThrowsForbiddenAccessException(string userId, string companyId)
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns(userId);

            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateCompanyCommand() { Id = companyId, Name = "UpdateCompanyName" };

            //Act
            Func<Task> func = async () => await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<ForbiddenAccessException>(func);
        }
    }
}