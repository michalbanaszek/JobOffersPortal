using FluentValidation.Results;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOfferPropositions.Commands
{
    public class UpdateJobOfferPropositionTest : BaseJobOfferPropositionInitialization
    {
        private readonly ILogger<UpdateJobOfferPropositionCommandHandler> _logger;
        private readonly UpdateJobOfferPropositionCommandValidator _validator;

        public UpdateJobOfferPropositionTest()
        {
            _logger = new Mock<ILogger<UpdateJobOfferPropositionCommandHandler>>().Object;
            _validator = new UpdateJobOfferPropositionCommandValidator();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_UpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = "Updated 1"
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var entity = await _mockJobOfferPropositionRepository.Object.GetByIdAsync(command.Id);

            //Assert
            response.Succeeded.ShouldBeTrue();
            validatorResult.IsValid.ShouldBeTrue();
            entity.Content.ShouldBe("Updated 1");            
        }

        [Fact]
        public async Task Handle_InvalidEmptyContent_NotUpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = string.Empty
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' must not be empty.");
        }

        [Fact]
        public async Task Handle_InvalidFormatContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = "Test /"
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' is not in the correct format.");
        }

        [Fact]
        public async Task Handle_InvalidMinLengthContent_NotUpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = new string('a', 1)            
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();         
            validatorResult.Errors[0].ErrorMessage.ShouldBe("The length of 'Content' must be at least 2 characters. You entered 1 characters.");
        }

        [Fact]
        public async Task Handle_InvalidMaxLengthContent_NotUpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = new string('a', 51)
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            //Assert
            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBeFalse();
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Content Length is between 2 and 50");
        }

        private static async Task<UpdateJobOfferPropositionCommandResponse> CheckValidationResult(UpdateJobOfferPropositionCommandHandler handler, UpdateJobOfferPropositionCommand command, ValidationResult validatorResult)
        {
            UpdateJobOfferPropositionCommandResponse response = null;

            if (validatorResult.IsValid)
            {
                response = await handler.Handle(command, CancellationToken.None);
            }

            return response;
        }
    }
}
