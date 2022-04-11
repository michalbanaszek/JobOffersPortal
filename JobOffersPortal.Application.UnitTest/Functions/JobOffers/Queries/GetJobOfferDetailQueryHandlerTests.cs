using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Queries
{
    public class GetJobOfferDetailQueryHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ILogger<GetJobOfferDetailQueryHandler>> _mockLogger;     
        private readonly IMapper _mapper;

        public GetJobOfferDetailQueryHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferDetailQueryHandler>>();         

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetJobOfferListTest()
        {
            var handler = new GetJobOfferDetailQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            var result = await handler.Handle(new GetJobOfferDetailQuery() {  Id = "1" }, CancellationToken.None);

            result.ShouldBeOfType<JobOfferViewModel>();
        }
    }
}
