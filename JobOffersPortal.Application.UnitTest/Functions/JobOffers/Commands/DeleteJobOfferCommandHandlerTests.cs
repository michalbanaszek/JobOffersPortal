using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
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
        private readonly IMapper _mapper;

        public DeleteJobOfferCommandHandlerTests()
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
            var result = await handler.Handle(command, CancellationToken.None);

            var jobOffersListCountAfterDelete = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Assert
            jobOffersListCountAfterDelete.ShouldNotBe(jobOffersListCountBeforeDelete);

            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteJobOfferCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }

        [Fact]
        public void Handle_NotOwnerUser_ReturnsForbiddenAccessException()
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteJobOfferCommandHandler(_mockJobOfferRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteJobOfferCommand() { Id = "1" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<ForbiddenAccessException>(() => func.Invoke());
        }
    }
}
