using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail;
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
    public class GetJobOfferSkillDetailQueryHandlerTests
    {
        private readonly Mock<IJobOfferSkillRepository> _mockJobOfferSkillRepository;
        private readonly Mock<ILogger<GetJobOfferSkillDetailQueryHandler>> _mockLogger;
        private readonly IMapper _mapper;

        public GetJobOfferSkillDetailQueryHandlerTests()
        {
            _mockJobOfferSkillRepository = MockJobOfferSkillRepository.GetJobOfferSkillRepository();
            _mockLogger = new Mock<ILogger<GetJobOfferSkillDetailQueryHandler>>();

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
            var handler = new GetJobOfferSkillDetailQueryHandler(_mockJobOfferSkillRepository.Object, _mapper, _mockLogger.Object);

            //Act
            var result = await handler.Handle(new GetJobOfferSkillDetailQuery() { Id = "1" }, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<JobOfferSkillDetailViewModel>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new GetJobOfferSkillDetailQueryHandler(_mockJobOfferSkillRepository.Object, _mapper, _mockLogger.Object);

            //Act
            Func<Task> func = () => handler.Handle(new GetJobOfferSkillDetailQuery() { Id = "99" }, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
