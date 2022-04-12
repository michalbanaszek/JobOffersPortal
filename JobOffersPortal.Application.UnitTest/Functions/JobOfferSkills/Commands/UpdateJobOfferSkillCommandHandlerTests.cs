using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferSkills.Commands
{
    public class UpdateJobOfferSkillCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IJobOfferSkillRepository> _mockJobOfferSkillRepository;
        private readonly Mock<ILogger<UpdateJobOfferSkillCommandHandler>> _mockLogger;

        public UpdateJobOfferSkillCommandHandlerTests()
        {
            _mockJobOfferSkillRepository = MockJobOfferSkillRepository.GetJobOfferSkillRepository();
            _mockLogger = new Mock<ILogger<UpdateJobOfferSkillCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferSkill_AddedToJobOfferSkillRepository()
        {
            //Arrange     
            var handler = new UpdateJobOfferSkillCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferSkillRepository.Object);

            var command = new UpdateJobOfferSkillCommand() { Id = "1", Content = "Test" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferSkillId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new UpdateJobOfferSkillCommandHandler(_mapper, _mockLogger.Object, _mockJobOfferSkillRepository.Object);

            var command = new UpdateJobOfferSkillCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }
    }
}
