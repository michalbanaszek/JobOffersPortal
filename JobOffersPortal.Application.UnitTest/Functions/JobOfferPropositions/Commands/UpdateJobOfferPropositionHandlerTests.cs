using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferPropositions.Commands
{
    public class UpdateJobOfferPropositionHandlerTests
    {
        private readonly Mock<ILogger<UpdateJobOfferPropositionCommandHandler>> _logger;
        private readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;
        private readonly IMapper _mapper;
        private readonly UpdateJobOfferPropositionCommandValidator _validator;

        public UpdateJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _logger = new Mock<ILogger<UpdateJobOfferPropositionCommandHandler>>();
            _validator = new UpdateJobOfferPropositionCommandValidator();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_UpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = "Updated 1"
            };

            var validatorResult = await _validator.ValidateAsync(command);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            var entity = await _mockJobOfferPropositionRepository.Object.GetByIdAsync(command.Id);

            validatorResult.IsValid.ShouldBeTrue();

            entity.Content.ShouldBe("Updated 1");
        }

        [Fact]
        public async Task Handle_InvalidEmptyContent_NotUpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = string.Empty
            };

            var validatorResult = await _validator.ValidateAsync(command);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert          
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' must not be empty.");
        }

        [Fact]
        public async Task Handle_InvalidFormatContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = "Test /"
            };

            var validatorResult = await _validator.ValidateAsync(command);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert          
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' is not in the correct format.");
        }

        [Fact]
        public async Task Handle_InvalidMinLengthContent_NotUpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = new string('a', 1)
            };

            var validatorResult = await _validator.ValidateAsync(command);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert          
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("The length of 'Content' must be at least 2 characters. You entered 1 characters.");
        }

        [Fact]
        public async Task Handle_InvalidMaxLengthContent_NotUpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = new string('a', 51)
            };

            var validatorResult = await _validator.ValidateAsync(command);

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert         
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Content Length is between 2 and 50");
        }
    }
}
