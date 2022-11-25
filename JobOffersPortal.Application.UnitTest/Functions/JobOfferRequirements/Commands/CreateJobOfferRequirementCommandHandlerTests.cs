using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferRequirements.Commands
{
    public class CreateJobOfferRequirementCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<IJobOfferRequirementRepository> _mockJobOfferRequirementRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<ILogger<CreateJobOfferRequirementCommandHandler>> _mockLogger;

        public CreateJobOfferRequirementCommandHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockJobOfferRequirementRepository = MockJobOfferRequirementRepository.GetJobOfferRequirementRepository();
            _mockUriService = MockUriService.GetUriService();
            _mockLogger = new Mock<ILogger<CreateJobOfferRequirementCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferRequirement_AddedToJobOfferRequirementRepository()
        {
            //Arrange     
            var handler = new CreateJobOfferRequirementCommandHandler(_mockJobOfferRepository.Object, _mockLogger.Object, _mockJobOfferRequirementRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferRequirementCommand() { JobOfferId = "1", Content = "Test" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CreateJobOfferRequirementCommandResponse>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new CreateJobOfferRequirementCommandHandler(_mockJobOfferRepository.Object, _mockLogger.Object, _mockJobOfferRequirementRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferRequirementCommand() { JobOfferId = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
