using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
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

        public CreateJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockUriService = MockUriService.GetUriService();
            _logger = new Mock<ILogger<CreateJobOfferPropositionCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_AddedToJobOfferPropositionRepository()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = "JobOfferProposition 1",
                JobOfferId = "1"
            };

            var itemsCountBefore = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Act          
            await handler.Handle(command, CancellationToken.None);

            var itemsCountAfter = (await _mockJobOfferPropositionRepository.Object.GetAllAsync()).Count;

            //Assert   
            itemsCountAfter.ShouldBe(itemsCountBefore + 1);
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_ReturnsSpecyficType()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand()
            {
                Content = "JobOfferProposition 1",
                JobOfferId = "1"
            };

            //Act          
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert   
            result.ShouldBeOfType<CreateJobOfferPropositionCommandResponse>();
        }


        [Fact]
        public void Handle_InvalidJobOfferPropositionId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new CreateJobOfferPropositionCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockJobOfferPropositionRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferPropositionCommand() { JobOfferId = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
