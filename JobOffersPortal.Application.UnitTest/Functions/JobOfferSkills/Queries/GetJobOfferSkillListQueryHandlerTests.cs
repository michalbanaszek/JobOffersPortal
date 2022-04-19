using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferSkills.Queries
{
    public class GetJobOfferSkillListQueryHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ILogger<GetJobOfferSkillListQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetJobOfferSkillListQueryHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferSkillListQueryHandler>>();

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
            var handler = new GetJobOfferSkillListQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferSkillListQuery() { JobOfferId = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferSkillViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new GetJobOfferSkillListQueryHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferSkillListQuery() { JobOfferId = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
