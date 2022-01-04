using Application.JobOffers.Commands.CreateJobOffer;
using FluentValidation.Results;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.JobOffers
{
    public class CreateJobOfferTest : BaseJobOfferInitialization
    {
        private readonly ILogger<CreateJobOfferCommandHandler> _logger;
        private readonly IUriJobOfferService _uriJobOfferService;
        private readonly CreateJobOfferCommandValidator _validator;

        public CreateJobOfferTest()
        {
            _logger = (new Mock<ILogger<CreateJobOfferCommandHandler>>()).Object;
            _uriJobOfferService = (new Mock<IUriJobOfferService>()).Object;
            _validator = new CreateJobOfferCommandValidator();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_AddedToJobOfferRepo()
        {
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriJobOfferService);

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

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            response.ShouldNotBeNull();
            validatorResult.IsValid.ShouldBe(true);
            jobListBeforeAdd.ShouldNotBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyPosition_NotAddedToJobOfferRepo()
        {
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriJobOfferService);

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

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' must not be empty.");
            validatorResult.Errors[1].ErrorMessage.ShouldBe("The length of 'Position' must be at least 2 characters. You entered 0 characters.");
            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptySalary_NotAddedToJobOfferRepo()
        {
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriJobOfferService);

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

            var jobListBeforeAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Salary' must not be empty.");
            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidEmptyPropositionRequirementSkill_NotAddedToJobOfferRepo()
        {
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriJobOfferService);

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

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Requirements' must not be empty.");
            validatorResult.Errors[1].ErrorMessage.ShouldBe("'Skills' must not be empty.");
            validatorResult.Errors[2].ErrorMessage.ShouldBe("'Propositions' must not be empty.");
            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidFormatPosition_NotAddedToJobOfferRepo()
        {
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriJobOfferService);

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

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("'Position' is not in the correct format.");
            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        [Fact]
        public async Task HandleValidator_InvalidMaxLengthPosition_NotAddedToJobOfferRepo()
        {
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger, _mockJobOfferRepository.Object, _uriJobOfferService);

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

            var validatorResult = await _validator.ValidateAsync(command);

            var response = await CheckValidationResult(handler, command, validatorResult);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            response.ShouldBeNull();
            validatorResult.IsValid.ShouldBe(false);
            validatorResult.Errors[0].ErrorMessage.ShouldBe("Position Length is between 2 and 30");
            jobListBeforeAdd.ShouldBe(jobListAfterAdd);
        }

        private static async Task<CreateJobOfferCommandResponse> CheckValidationResult(CreateJobOfferCommandHandler handler, CreateJobOfferCommand command, ValidationResult validatorResult)
        {
            CreateJobOfferCommandResponse createCompanyResponse = null;

            if (validatorResult.IsValid)
            {
                createCompanyResponse = await handler.Handle(command, CancellationToken.None);
            }

            return createCompanyResponse;
        }
    }
}