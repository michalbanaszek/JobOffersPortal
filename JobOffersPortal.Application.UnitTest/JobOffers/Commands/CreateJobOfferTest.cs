﻿using Application.JobOffers.Commands.CreateJobOffer;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOffers.Commands
{
    public class CreateJobOfferTest : BaseJobOfferInitialization
    {
        private readonly ILogger<CreateJobOfferCommandHandler> _logger;
        private readonly IUriService _uriService;
        private readonly CreateJobOfferCommandValidator _validator;

        public CreateJobOfferTest()
        {
            _logger = new Mock<ILogger<CreateJobOfferCommandHandler>>().Object;
            _uriService = new Mock<IUriService>().Object;
            _validator = new CreateJobOfferCommandValidator();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_AddedToJobOfferRepo()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriService);

            var command = new CreateJobOfferCommand()
            {
                CompanyId = "1",
                Position = "Test",
                Salary = "1100",
                IsAvailable = true,
                Date = DateTime.Now,
                Propositions = new string[] { "PropositionTest" },
                Requirements = new string[] { "RequirementTest" },
                Skills = new string[] { "SkillTest" }
            };

            var jobListBeforeAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            validatorResult.IsValid.ShouldBeTrue();

            jobListBeforeAdd.ShouldNotBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_ValidEmptySalary_AddedToJobOfferRepo()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriService);

            var command = new CreateJobOfferCommand()
            {
                CompanyId = "1",
                Position = "PositionTest",
                Salary = "",
                IsAvailable = true,
                Date = DateTime.Now,
                Propositions = new string[] { "PropositionTest" },
                Requirements = new string[] { "RequirementTest" },
                Skills = new string[] { "SkillTest" }
            };

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            validatorResult.IsValid.ShouldBeTrue();
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyPosition_NotAddedToJobOfferRepo()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriService);

            var command = new CreateJobOfferCommand()
            {
                CompanyId = "1",
                Position = "",
                Salary = "1000",
                IsAvailable = true,
                Date = DateTime.Now,
                Propositions = new string[] { "PropositionTest" },
                Requirements = new string[] { "RequirementTest" },
                Skills = new string[] { "SkillTest" }
            };

            var jobListBeforeAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;   
            
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' must not be empty.");

            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Position' must be at least 2 characters. You entered 0 characters.");

            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyPropositionRequirementSkill_NotAddedToJobOfferRepo()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriService);

            var command = new CreateJobOfferCommand()
            {
                CompanyId = "1",
                Position = "PositionTest",
                Salary = "1000",
                IsAvailable = true,
                Date = DateTime.Now,
                Propositions = null,
                Requirements = null,
                Skills = null
            };

            var jobListBeforeAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;    
            
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Requirements' must not be empty.");

            validatorResult.Errors[1].ErrorMessage.ShouldBe("'Skills' must not be empty.");

            validatorResult.Errors[2].ErrorMessage.ShouldBe("'Propositions' must not be empty.");

            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatPosition_NotAddedToJobOfferRepo()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriService);

            var command = new CreateJobOfferCommand()
            {
                CompanyId = "1",
                Position = new string('*', 10),
                Salary = "1000",
                IsAvailable = true,
                Date = DateTime.Now,
                Propositions = new string[] { "PropositionTest" },
                Requirements = new string[] { "RequirementTest" },
                Skills = new string[] { "SkillTest" }
            };

            var jobListBeforeAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;     
            
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' is not in the correct format.");

            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthPosition_NotAddedToJobOfferRepo()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriService);

            var command = new CreateJobOfferCommand()
            {
                CompanyId = "1",
                Position = new string('a', 31),
                Salary = "1000",
                IsAvailable = true,
                Date = DateTime.Now,
                Propositions = new string[] { "PropositionTest" },
                Requirements = new string[] { "RequirementTest" },
                Skills = new string[] { "SkillTest" }
            };

            var jobListBeforeAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Act
            var validatorResult = await _validator.ValidateAsync(command);

            if (validatorResult.IsValid)
            {
                await handler.Handle(command, CancellationToken.None);
            }

            //Assert
            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;       
            
            validatorResult.IsValid.ShouldBeFalse();

            validatorResult.Errors[0].ErrorMessage.ShouldBe("Position Length is between 2 and 30");

            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }
    }
}