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

        public UpdateJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _logger = new Mock<ILogger<UpdateJobOfferPropositionCommandHandler>>();      

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_UpdatedToJobOfferPropositionRepository()
        {
            //Arrange
            var handler = new UpdateJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new UpdateJobOfferPropositionCommand()
            {
                Id = "1",
                Content = "Updated 1"
            };

            //Act
            await handler.Handle(command, CancellationToken.None);

            var entity = await _mockJobOfferPropositionRepository.Object.GetByIdAsync(command.Id);

            //Assert
            entity.Content.ShouldBe("Updated 1");
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_ReturnSpecyficType()
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

            //Assert
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
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
