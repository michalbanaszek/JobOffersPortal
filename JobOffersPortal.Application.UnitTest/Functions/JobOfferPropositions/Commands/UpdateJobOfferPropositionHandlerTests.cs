using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
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
    public class UpdateJobOfferPropositionHandlerTests
    {
        private readonly Mock<ILogger<UpdateJobOfferPropositionCommandHandler>> _logger;
        private readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;
        private readonly IMapper _mapper;
        private readonly UpdateJobOfferPropositionCommandValidator _validator;

        public UpdateJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _logger = new Mock<ILogger<UpdateJobOfferPropositionCommandHandler>>();
            _validator = new UpdateJobOfferPropositionCommandValidator();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_UpdatedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = "Updated 1"
            };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            var entity = await _mockJobOfferPropositionRepository.Object.GetByIdAsync(command.Id);

            //Assert
            entity.Content.ShouldBe("Updated 1");

            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferPropositionId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }
    }
}
