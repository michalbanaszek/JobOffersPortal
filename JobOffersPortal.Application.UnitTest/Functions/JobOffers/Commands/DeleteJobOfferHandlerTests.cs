using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Commands
{
    public class DeleteJobOfferHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<DeleteJobOfferCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public DeleteJobOfferHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<DeleteJobOfferCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_DeletedToJobOfferRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

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
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

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
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

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
