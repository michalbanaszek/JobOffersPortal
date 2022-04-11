using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Queries
{
    public class GetCompanyQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ILogger<GetCompanyQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockLogger = new Mock<ILogger<GetCompanyQueryHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetJobOfferListTest()
        {
            var handler = new GetCompanyQueryHandler(_mockCompanyRepository.Object, _mapper, _mockLogger.Object);

            var result = await handler.Handle(new GetCompanyQuery() { Id = "1" }, CancellationToken.None);

            result.ShouldBeOfType<CompanyDetailViewModel>();
        }
    }
}
