using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOffers.Commands
{
    public class DeleteJobOfferTest : BaseJobOfferInitialization
    {
        private readonly ILogger<DeleteJobOfferCommandHandler> _logger;

        public DeleteJobOfferTest()
        {
            _logger = new Mock<ILogger<DeleteJobOfferCommandHandler>>().Object;
        }

        [Fact]
        public async Task Handle_ValidJobOffer_DeletedToJobOfferRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger, _currentUserService);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            var jobOffersListCountBeforeDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            var jobOffersListCountAfterDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            jobOffersListCountAfterDelete.ShouldNotBe(jobOffersListCountBeforeDelete);
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidJobOffer_DeletedToJobOfferRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger, _currentUserService);

            var command = new DeleteJobOfferCommand() { Id = "99" };

            NotFoundException exceptionResponse = null;

            //Act
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException exception)
            {
                exceptionResponse = exception;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (99) was not found.");
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotDeletedToJobOfferRepo()
        {
            //Arrange
            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger, _currentUserService);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            ForbiddenAccessException exceptionResponse = null;

            //Act
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (ForbiddenAccessException exception)
            {
                exceptionResponse = exception;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (1) do not own this entity.");
        }
    }
}
