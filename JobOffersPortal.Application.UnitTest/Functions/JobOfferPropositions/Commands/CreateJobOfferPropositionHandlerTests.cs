using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferPropositions.Commands
{
    public class CreateJobOfferPropositionHandlerTests
    {
        private readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<ILogger<CreateJobOfferPropositionCommandHandler>> _logger;
        private readonly IMapper _mapper;
        private readonly CreateJobOfferPropositionCommandValidator _validator;

        public CreateJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockUriService = MockUriService.GetUriService();
            _logger = new Mock<ILogger<CreateJobOfferPropositionCommandHandler>>();
            _validator = new CreateJobOfferPropositionCommandValidator();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_AddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = "JobOfferProposition 1",
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert   
            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeTrue();

            itemsCountBefore.ShouldNotBe(itemsCountAfter);
        }

        [Fact]
        public async Task Handle_InvalidEmptyContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = string.Empty,
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert  
            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeFalse();

            itemsCountBefore.ShouldBe(itemsCountAfter);

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' must not be empty.");
        }

        [Fact]
        public async Task Handle_InvalidFormatContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = "Test /",
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert  
            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeFalse();

            itemsCountBefore.ShouldBe(itemsCountAfter);

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Content' is not in the correct format.");
        }

        [Fact]
        public async Task Handle_InvalidMinLengthContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = new string('a', 1),
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert  
            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeFalse();

            itemsCountBefore.ShouldBe(itemsCountAfter);

            validatorResult.Errors[0].ErrorMessage.ShouldBe("The length of 'Content' must be at least 2 characters. You entered 1 characters.");
        }

        [Fact]
        public async Task Handle_InvalidMaxLengthContent_NotAddedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = new string('a', 51),
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert  
            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeFalse();

            itemsCountBefore.ShouldBe(itemsCountAfter);

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Content Length is between 2 and 50");
        }
    }
}
