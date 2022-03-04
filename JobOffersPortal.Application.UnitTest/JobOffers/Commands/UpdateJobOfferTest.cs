using FluentValidation.Results;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOffers.Commands
{
    public class UpdateJobOfferTest : BaseJobOfferInitialization
    {
        private readonly ILogger<UpdateJobOfferCommandHandler> _logger;
        private readonly IUriJobOfferService _uriJobOfferService;
        private readonly UpdateJobOfferCommandValidator _validator;

        public UpdateJobOfferTest()
        {
            _logger = new Mock<ILogger<UpdateJobOfferCommandHandler>>().Object;
            _uriJobOfferService = new Mock<IUriJobOfferService>().Object;
            _validator = new UpdateJobOfferCommandValidator();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_UpdatedToJobOfferRepo()
        {
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateJobOfferCommand()
            {
                Id = "1",
                Position = "PositionTest",
                IsAvailable = true,
                Salary = "1000"
            };

            var result = await handler.Handle(command, CancellationToken.None);

            var entityUpdated = await _mockJobOfferRepository.Object.GetByIdAsync(command.Id);

            result.Succeeded.ShouldBe(true);
            entityUpdated.Id.ShouldBe("1");
            entityUpdated.Position.ShouldBe("PositionTest");
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyPosition_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = string.Empty,
                Salary = "1000",
                Date = DateTime.Now,
                IsAvailable = true
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' must not be empty.");
            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Position' must be at least 2 characters. You entered 0 characters.");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatPosition_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = new string('*', 10),
                Salary = "1000",
                Date = DateTime.Now,
                IsAvailable = true
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' is not in the correct format.");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthPosition_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = new string('a', 31),
                Salary = "1000",
                Date = DateTime.Now,
                IsAvailable = true
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Position Length is between 2 and 30");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptySalary_NotUpdatedToCompanyRepo()
        {
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var command = new UpdateJobOfferCommand()
            {
                Position = "PositionTest",
                Salary = string.Empty,
                Date = DateTime.Now,
                IsAvailable = true
            };

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var allCompanies = await _mockJobOfferRepository.Object.GetAllAsync();

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Salary' must not be empty.");
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount);
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidJobOffer_NotUpdatedToJobOfferRepo()
        {
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateJobOfferCommand() { Id = "99" };

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
                exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (99) was not found.");
            }
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotUpdatedToJobOfferRepo()
        {
            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user2");

            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _currentUserService);

            var command = new UpdateJobOfferCommand() { Id = "1" };

            ForbiddenAccessException exceptionResponse = null;

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
                exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (1) do not own this entity.");
            }
        }

        private static async Task<UpdateJobOfferCommandResponse> CheckValidationResult(UpdateJobOfferCommandHandler handler, UpdateJobOfferCommand command, ValidationResult validatorResult)
        {
            UpdateJobOfferCommandResponse updateCompanyResponse = null;

            if (validatorResult.IsValid)
            {
                updateCompanyResponse = await handler.Handle(command, CancellationToken.None);
            }

            return updateCompanyResponse;
        }
    }
}
