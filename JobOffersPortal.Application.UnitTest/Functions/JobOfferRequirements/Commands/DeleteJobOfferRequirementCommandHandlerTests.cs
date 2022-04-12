﻿using Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOfferRequirements.Commands
{
    public class DeleteJobOfferRequirementCommandHandlerTests
    {
        private readonly IMapper _mapper;     
        private readonly Mock<IJobOfferRequirementRepository> _mockJobOfferRequirementRepository;      
        private readonly Mock<ILogger<DeleteJobOfferRequirementCommandHandler>> _mockLogger;

        public DeleteJobOfferRequirementCommandHandlerTests()
        {      
            _mockJobOfferRequirementRepository = MockJobOfferRequirementRepository.GetJobOfferRequirementRepository();          
            _mockLogger = new Mock<ILogger<DeleteJobOfferRequirementCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOfferRequirement_AddedToJobOfferRequirementRepository()
        {
            //Arrange     
            var handler = new DeleteJobOfferRequirementCommandHandler(_mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new DeleteJobOfferRequirementCommand() { Id = "1" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidJobOfferRequirementId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new DeleteJobOfferRequirementCommandHandler(_mockLogger.Object, _mockJobOfferRequirementRepository.Object);

            var command = new DeleteJobOfferRequirementCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }
    }
}