using Application.JobOfferPropositions.Commands.CreateJobOfferProposition;
using FluentValidation.Results;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOfferPropositions.Commands
{
    public class CreateJobOfferPropositionTest : BaseJobOfferPropositionInitialization
    {
        private readonly ILogger<CreateJobOfferPropositionCommandHandler> _logger;
        private readonly CreateJobOfferPropositionCommandValidator _validator;

        public CreateJobOfferPropositionTest()
        {
            _logger = new Mock<ILogger<CreateJobOfferPropositionCommandHandler>>().Object;
            _validator = new CreateJobOfferPropositionCommandValidator();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_AddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = "JobOfferProposition 1",
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Assert
            response.Succeeded.ShouldBeTrue();
            validatorResult.IsValid.ShouldBeTrue();
            itemsCountBefore.ShouldNotBe(itemsCountAfter);
        }

        [Fact]
        public async Task Handle_InvalidEmptyContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = string.Empty,
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            itemsCountBefore.ShouldBe(itemsCountAfter);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' must not be empty.");
        }

        [Fact]
        public async Task Handle_InvalidFormatContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = "Test /",
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            itemsCountBefore.ShouldBe(itemsCountAfter);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' is not in the correct format.");
        }

        [Fact]
        public async Task Handle_InvalidMinLengthContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = new string('a', 1),
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            itemsCountBefore.ShouldBe(itemsCountAfter);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("The length of 'Content' must be at least 2 characters. You entered 1 characters.");
        }

        [Fact]
        public async Task Handle_InvalidMaxLengthContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = new string('a', 51),
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            itemsCountBefore.ShouldBe(itemsCountAfter);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Content Length is between 2 and 50");
        }

        private static async Task<CreateJobOfferPropositionCommandResponse> CheckValidationResult(CreateJobOfferPropositionCommandHandler handler, CreateJobOfferPropositionCommand command, ValidationResult validatorResult)
        {
            CreateJobOfferPropositionCommandResponse response = null;

            if (validatorResult.IsValid)
            {
                response = await handler.Handle(command, CancellationToken.None);
            }

            return response;
        }
    }
}
