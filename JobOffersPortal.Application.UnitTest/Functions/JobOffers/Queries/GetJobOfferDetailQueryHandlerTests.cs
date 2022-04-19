using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
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
        public async Task Handle_GetJobOfferDetail_ReturnSpecyficType()
        {
            //Arrange
            var handler = new GetJobOfferDetailQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferDetailQuery() { Id = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new GetJobOfferDetailQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferDetailQuery() { Id = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
