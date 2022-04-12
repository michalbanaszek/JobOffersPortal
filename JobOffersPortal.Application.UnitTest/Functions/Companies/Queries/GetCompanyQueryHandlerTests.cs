using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Queries
{
    public class GetCompanyQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ILogger<GetCompanyDetailQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetCompanyQueryHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockLogger = new Mock<ILogger<GetCompanyDetailQueryHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetCompanyDetail_ReturnsSpecificType()
        {
            //Arrange
            var handler = new GetCompanyDetailQueryHandler(_mockCompanyRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetCompanyDetailQuery() { Id = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CompanyDetailViewModel>();
        }

        [Fact]
        public void Handle_InvalidCompanyId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new GetCompanyDetailQueryHandler(_mockCompanyRepository.Object, _mapper, _mockLogger.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetCompanyDetailQuery() { Id = "99" }, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }
    }
}
