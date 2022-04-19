using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferRequirements.Queries
{
    public class GetJobOfferRequirementListQueryHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ILogger<GetJobOfferRequirementListQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetJobOfferRequirementListQueryHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferRequirementListQueryHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetJobOfferRequirementList_ReturnsSpecificType()
        {
            //Arrange
            var handler = new GetJobOfferRequirementListQueryHandler(_mapper, _mockJobOfferRepository.Object, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferRequirementListQuery() { JobOfferId = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferRequirementViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new GetJobOfferRequirementListQueryHandler(_mapper, _mockJobOfferRepository.Object, _mockLogger.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferRequirementListQuery() { JobOfferId = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
