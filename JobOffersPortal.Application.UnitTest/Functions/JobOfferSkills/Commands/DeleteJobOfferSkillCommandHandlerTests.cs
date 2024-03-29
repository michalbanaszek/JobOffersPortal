﻿using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill;
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
    public class DeleteJobOfferSkillCommandHandlerTests
    {     
        private readonly Mock<IJobOfferSkillRepository> _mockJobOfferSkillRepository;
        private readonly Mock<ILogger<DeleteJobOfferSkillCommandHandler>> _mockLogger;

        public DeleteJobOfferSkillCommandHandlerTests()
        {
            _mockJobOfferSkillRepository = MockJobOfferSkillRepository.GetJobOfferSkillRepository();
            _mockLogger = new Mock<ILogger<DeleteJobOfferSkillCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ValidJobOfferSkill_ReturnsSpecyficType()
        {
            //Arrange     
            var handler = new DeleteJobOfferSkillCommandHandler(_mockLogger.Object, _mockJobOfferSkillRepository.Object);

            var command = new DeleteJobOfferSkillCommand() { Id = "1" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferSkillId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferSkillCommandHandler(_mockLogger.Object, _mockJobOfferSkillRepository.Object);

            var command = new DeleteJobOfferSkillCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }
    }
}
