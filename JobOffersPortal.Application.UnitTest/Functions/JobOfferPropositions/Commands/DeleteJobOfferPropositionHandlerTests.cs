using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition;
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
    public class DeleteJobOfferPropositionHandlerTests
    {
        private readonly Mock<ILogger<DeleteJobOfferPropositionCommandHandler>> _logger;
        private readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;     
        private readonly IMapper _mapper;

        public DeleteJobOfferPropositionHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();          
            _logger = new Mock<ILogger<DeleteJobOfferPropositionCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferProposition_ReturnsSpecyficType()
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
        public void Handle_InvalidJobOfferPropositionId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

            var command = new DeleteJobOfferPropositionCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
