using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferSkills.Commands
{
    public class CreateJobOfferSkillCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<IJobOfferSkillRepository> _mockJobOfferRequirementRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<ILogger<CreateJobOfferSkillCommandHandler>> _mockLogger;

        public CreateJobOfferSkillCommandHandlerTests()
        {
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockJobOfferRequirementRepository = MockJobOfferSkillRepository.GetJobOfferSkillRepository();
            _mockUriService = MockUriService.GetUriService();
            _mockLogger = new Mock<ILogger<CreateJobOfferSkillCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferSkill_ReturnsSpecyficType()
        {
            //Arrange     
            var handler = new CreateJobOfferSkillCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object, _mockJobOfferRequirementRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferSkillCommand() { JobOfferId = "1", Content = "Test" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<CreateJobOfferSkillCommandResponse>();
        }

        [Fact]
        public void Handle_InvalidJobOfferSkillId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new CreateJobOfferSkillCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferRepository.Object, _mockJobOfferRequirementRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferSkillCommand() { JobOfferId = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
