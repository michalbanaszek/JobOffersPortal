using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionDetail;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferPropositions.Queries
{
    public class GetJobOfferPropositionDetailQueryHandlerTests
    {
        private readonly Mock<IJobOfferPropositionRepository> _mockJobOfferPropositionRepository;
        private readonly Mock<ILogger<GetJobOfferPropositionDetailQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetJobOfferPropositionDetailQueryHandlerTests()
        {
            _mockJobOfferPropositionRepository = MockJobOfferPropositionRepository.GetJobOfferPropositionRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferPropositionDetailQueryHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetJobOfferPropositionDetail_ReturnsSpecificType()
        {
            //Arrange
            var handler = new GetJobOfferPropositionDetailQueryHandler(_mockJobOfferPropositionRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferPropositionDetailQuery() { Id = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferPropositionDetailViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferPropositionId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new GetJobOfferPropositionDetailQueryHandler(_mockJobOfferPropositionRepository.Object, _mapper, _mockLogger.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferPropositionDetailQuery() { Id = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}