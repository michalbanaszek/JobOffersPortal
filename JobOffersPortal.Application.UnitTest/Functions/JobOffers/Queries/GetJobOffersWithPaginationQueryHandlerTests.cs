using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Queries
{
    public class GetJobOffersWithPaginationQueryHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ILogger<GetJobOffersWithPaginationQueryHandler>> _mockLogger;
        private readonly Mock<IUriService> _mockUriService;
        private readonly IMapper _mapper;

        public GetJobOffersWithPaginationQueryHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockLogger = new Mock<ILogger<GetJobOffersWithPaginationQueryHandler>>();
            _mockUriService = MockUriService.GetUriService();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        // [Fact]
        public async Task Handle_GetJobOfferListWithPagination_ReturnsSpecyficType()
        {
            //Arrange
            var handler = new GetJobOffersWithPaginationQueryHandler(_mapper, _mockJobOfferRepository.Object, _mockUriService.Object, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetJobOffersWithPaginationQuery() { CompanyId = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<PaginatedList<JobOfferViewModel>>();
        }
    }
}