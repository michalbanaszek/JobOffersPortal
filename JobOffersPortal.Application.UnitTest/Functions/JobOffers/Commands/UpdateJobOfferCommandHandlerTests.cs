using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
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
    public class UpdateJobOfferCommandHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<UpdateJobOfferCommandHandler>> _logger;
        private readonly UpdateJobOfferCommandValidator _validator;
        private readonly IMapper _mapper;

        public UpdateJobOfferCommandHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<UpdateJobOfferCommandHandler>>();
            _validator = new UpdateJobOfferCommandValidator(_mockJobOfferRepository.Object);

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_UpdatedToJobOfferRepository()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand()
            {
                Id = "1",
                Position = "PositionTest",
                IsAvailable = true,
                Salary = "1000"
            };

            //Act         
            var result = await handler.Handle(command, CancellationToken.None);

            var entityUpdated = await _mockJobOfferRepository.Object.GetByIdAsync(command.Id);

            //Assert
            result.ShouldBeOfType<Unit>();

            entityUpdated.Id.ShouldBe("1");

            entityUpdated.Position.ShouldBe("PositionTest");
        }

        [Fact]
        public void Handle_InvalidJobOfferId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand() { Id = "99" };

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

            var handler = new UpdateJobOfferCommandHandler(_mockJobOfferRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateJobOfferCommand() { Id = "1" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<ForbiddenAccessException>(() => func.Invoke());
        }
    }
}
