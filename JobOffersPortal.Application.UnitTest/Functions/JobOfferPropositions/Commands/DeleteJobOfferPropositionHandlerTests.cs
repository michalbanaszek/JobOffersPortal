using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
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
        public async Task Handle_ValidJobOfferProposition_DeletedToJobOfferPropositionRepo()
        {
            //Arrange
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

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
            var handler = new DeleteJobOfferPropositionCommandHandler(_mapper, _logger.Object, _mockJobOfferPropositionRepository.Object);

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
