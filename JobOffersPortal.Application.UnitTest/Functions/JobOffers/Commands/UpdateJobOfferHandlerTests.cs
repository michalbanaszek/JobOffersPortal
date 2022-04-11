using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Commands
{
    public class UpdateJobOfferHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<UpdateJobOfferCommandHandler>> _logger;
        private readonly UpdateJobOfferCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateJobOfferHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<UpdateJobOfferCommandHandler>>();
            _validator = new UpdateJobOfferCommandValidator(_mockJobOfferRepository.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_UpdatedToJobOfferRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand()
            {
                Id = "1",
                Position = "PositionTest",
                IsAvailable = true,
                Salary = "1000"
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var entityUpdated = await _mockJobOfferRepository.Object.GetByIdAsync(command.Id);

            entityUpdated.Id.ShouldBe("1");

            entityUpdated.Position.ShouldBe("PositionTest");
        }

        [Fact]
        public async Task Handle_ValidEmptySalary_UpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand()
            {
                Id = "1",
                Position = "PositionTest",
                Salary = string.Empty,
                Date = DateTime.Now,
                IsAvailable = true
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            validatorResult.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyPosition_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = string.Empty,
                Salary = "1000",
                Date = DateTime.Now,
                IsAvailable = true
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' must not be empty.");

            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Position' must be at least 2 characters. You entered 0 characters.");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatPosition_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = new string('*', 10),
                Salary = "1000",
                Date = DateTime.Now,
                IsAvailable = true
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' is not in the correct format.");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthPosition_NotUpdatedToCompanyRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = new string('a', 31),
                Salary = "1000",
                Date = DateTime.Now,
                IsAvailable = true
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Position Length is between 2 and 30");

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidJobOffer_NotUpdatedToJobOfferRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand() { Id = "99" };

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

            exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (99) was not found.");
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotUpdatedToJobOfferRepo()
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand() { Id = "1" };

            ForbiddenAccessException exceptionResponse = null;

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

            exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (1) do not own this entity.");
        }
    }
}
