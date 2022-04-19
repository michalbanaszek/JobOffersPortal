using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail;
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
    public class GetJobOfferRequirementDetailQueryHandlerTests
    {
        private readonly Mock<IJobOfferRequirementRepository> _mockJobOfferRequirementRepository;
        private readonly Mock<ILogger<GetJobOfferRequirementDetailQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetJobOfferRequirementDetailQueryHandlerTests()
        {
            _mockJobOfferRequirementRepository = MockJobOfferRequirementRepository.GetJobOfferRequirementRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferRequirementDetailQueryHandler>>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_GetJobOfferRequirementDetail_ReturnsSpecificType()
        {
            //Arrange
            var handler = new GetJobOfferRequirementDetailQueryHandler(_mockJobOfferRequirementRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferRequirementDetailQuery() { Id = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferRequirementDetailViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new GetJobOfferRequirementDetailQueryHandler(_mockJobOfferRequirementRepository.Object, _mapper, _mockLogger.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferRequirementDetailQuery() { Id = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
