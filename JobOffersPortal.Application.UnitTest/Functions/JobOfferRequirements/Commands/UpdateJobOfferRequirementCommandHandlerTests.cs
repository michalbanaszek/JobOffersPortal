using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferRequirements.Commands
{
    public class UpdateJobOfferRequirementCommandHandlerTests
    {
        private readonly IMapper _mapper;       
        private readonly Mock<IJobOfferRequirementRepository> _mockJobOfferRequirementRepository;        
        private readonly Mock<ILogger<UpdateJobOfferRequirementCommandHandler>> _mockLogger;

        public UpdateJobOfferRequirementCommandHandlerTests()
        {         
            _mockJobOfferRequirementRepository = MockJobOfferRequirementRepository.GetJobOfferRequirementRepository();       
            _mockLogger = new Mock<ILogger<UpdateJobOfferRequirementCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferRequirement_UpdatedToJobOfferRequirementRepository()
        {
            //Arrange
            var handler = new UpdateJobOfferRequirementCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new UpdateJobOfferRequirementCommand()
            {
                Id = "1",
                Content = "Updated 1"
            };

            //Act
            await handler.Handle(command, CancellationToken.None);

            var entity = await _mockJobOfferRequirementRepository.Object.GetByIdAsync(command.Id);

            //Assert
            entity.Content.ShouldBe("Updated 1");
        }

        [Fact]
        public async Task Handle_ValidJobOfferRequirement_ReturnsSpecyficType()
        {
            //Arrange     
            var handler = new UpdateJobOfferRequirementCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new UpdateJobOfferRequirementCommand() { Id = "1", Content = "Test" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new UpdateJobOfferRequirementCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new UpdateJobOfferRequirementCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
