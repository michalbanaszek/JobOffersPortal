using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList;
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
    public class GetJobOfferPropositionListQueryHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ILogger<GetJobOfferPropositionListQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetJobOfferPropositionListQueryHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferPropositionListQueryHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetJobOfferPropositionList_ReturnsSpecificType()
        {
            //Arrange 
            var handler = new GetJobOfferPropositionListQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferPropositionListQuery() { JobOfferId = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferPropositionViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferPropositionId_ThrowsNotFoundException()
        {
            //Arrange 
            var handler = new GetJobOfferPropositionListQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferPropositionListQuery() { JobOfferId = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
