using AutoMapper;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Queries
{
    public class GetCompaniesListWithPaginationQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly IMapper _mapper;

        public GetCompaniesListWithPaginationQueryHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockUriService = MockUriService.GetUriService();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        // [Fact]
        public async Task Handle_GetCompaniesListWithPagination_ReturnsSpecificType()
        {
            //Arrange
            var handler = new GetCompaniesListWithPaginationQueryHandler(_mapper, _mockUriService.Object, _mockCompanyRepository.Object);

            //Act
            var result = await handler.Handle(new GetCompaniesListWithPaginationQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<PaginatedList<CompanyJobOfferListViewModel>>();
        }
    }
}
