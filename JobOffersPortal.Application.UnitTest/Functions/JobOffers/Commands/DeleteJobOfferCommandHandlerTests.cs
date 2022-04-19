using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Commands
{
    public class DeleteJobOfferCommandHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<DeleteJobOfferCommandHandler>> _logger;       

        public DeleteJobOfferCommandHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<DeleteJobOfferCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_DeletedToJobOfferRepository()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            var jobOffersListCountBeforeDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            await handler.Handle(command, CancellationToken.None);

            var jobOffersListCountAfterDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Assert
            jobOffersListCountAfterDelete.ShouldBe(jobOffersListCountBeforeDelete -1);
        }

        [Fact]
        public async Task Handle_ValidJobOffer_ReturnsSpecyficType()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert    
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteJobOfferCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public void Handle_NotOwnerUser_ThrowsForbiddenAccessException()
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<ForbiddenAccessException>();
        }
    }
}
