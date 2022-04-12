using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition;
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

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferPropositions.Commands
{
    public class DeleteJobOfferPropositionHandlerTests
    {
        private readonly Mock<ILogger<DeleteJobOfferPropositionCommandHandler>> _logger;
        private readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly IMapper _mapper;

        public DeleteJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<DeleteJobOfferPropositionCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_DeletedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new DeleteJobOfferPropositionCommand() { Id = "1" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferPropositionId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new DeleteJobOfferPropositionCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }
    }
}
