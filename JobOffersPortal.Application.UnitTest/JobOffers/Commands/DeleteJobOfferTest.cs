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
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger, _currentUserService);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            var jobOffersListCountBeforeDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var response = await handler.Handle(command, CancellationToken.None);

            var jobOffersListCountAfterDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            jobOffersListCountAfterDelete.ShouldNotBe(jobOffersListCountBeforeDelete);
            response.ShouldNotBeNull();
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidJobOffer_DeletedToJobOfferRepo()
        {
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger, _currentUserService);

            var command = new DeleteJobOfferCommand() { Id = "99" };

            NotFoundException exceptionResponse = null;

            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException exception)
            {
                exceptionResponse = exception;
            }
            finally
            {
                exceptionResponse.ShouldNotBeNull();
                exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (99) was not found.");
            }
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotDeletedToJobOfferRepo()
        {
            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger, _currentUserService);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            ForbiddenAccessException exceptionResponse = null;

            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (ForbiddenAccessException exception)
            {
                exceptionResponse = exception;
            }
            finally
            {
                exceptionResponse.ShouldNotBeNull();
                exceptionResponse.Message.ShouldBe("Entity \"JobOffer\" (1) do not own this entity.");
            }
        }
    }
}
