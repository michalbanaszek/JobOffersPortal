using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOfferPropositions.Commands
{
    public class DeleteJobOfferPropositionTest : BaseJobOfferPropositionInitialization
    {
        private readonly ILogger<DeleteJobOfferPropositionCommandHandler> _logger;

        public DeleteJobOfferPropositionTest()
        {
            _logger = (new Mock<ILogger<DeleteJobOfferPropositionCommandHandler>>()).Object;
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_DeletedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object, _currentUserServiceMock.Object);

            var command = new DeleteJobOfferPropositionCommand() { Id = "1" };

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            var entity = await _mockJobOfferPropositionRepository.Object.GetByIdAsync("1");

            entity.ShouldBeNull();
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidJbOfferPropositionId_NotDeletedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger, _mockJobOfferPropositionRepository.Object, _currentUserServiceMock.Object);

            var command = new DeleteJobOfferPropositionCommand() { Id = "10" };

            NotFoundException exceptionResponse = null;

            //Act
            try
            {
                var response = await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException ex)
            {
                exceptionResponse = ex;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"JobOfferProposition\" (10) was not found.");
        }
    }
}
