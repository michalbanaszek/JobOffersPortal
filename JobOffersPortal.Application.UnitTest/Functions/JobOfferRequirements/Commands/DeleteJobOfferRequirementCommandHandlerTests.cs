using Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
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
    public class DeleteJobOfferRequirementCommandHandlerTests
    {        
        private readonly Mock<IJobOfferRequirementRepository> _mockJobOfferRequirementRepository;      
        private readonly Mock<ILogger<DeleteJobOfferRequirementCommandHandler>> _mockLogger;

        public DeleteJobOfferRequirementCommandHandlerTests()
        {      
            _mockJobOfferRequirementRepository = MockJobOfferRequirementRepository.GetJobOfferRequirementRepository();          
            _mockLogger = new Mock<ILogger<DeleteJobOfferRequirementCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ValidJobOfferRequirement_ReturnsSpecyficType()
        {
            //Arrange     
            var handler = new DeleteJobOfferRequirementCommandHandler(_mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new DeleteJobOfferRequirementCommand() { Id = "1" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferRequirementCommandHandler(_mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new DeleteJobOfferRequirementCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
