using Application.JobOffers.Commands.CreateJobOffer;
using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.JobOffers.Commands
{
    public class CreateJobOfferCommandHandlerTests
    {
        private readonly Mock<IJobOfferRepository> _mockJobOfferRepository;
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<IUriService> _mockUriService;
        private readonly Mock<ILogger<CreateJobOfferCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public CreateJobOfferCommandHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockJobOfferRepository = MockJobOfferRepository.GetJobOffersRepository();
            _mockUriService = MockUriService.GetUriService();
            _logger = new Mock<ILogger<CreateJobOfferCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidJobOffer_AddedToJobOfferRepository()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger.Object, _mockJobOfferRepository.Object, _mockCompanyRepository.Object, _mockUriService.Object);

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
            var result = await handler.Handle(command, CancellationToken.None);

            var jobListAfterAdd = (await _mockJobOfferRepository.Object.GetAllAsync()).Count;

            //Assert
            jobListAfterAdd.ShouldNotBe(jobListBeforeAdd);

            result.ShouldBeOfType<CreateJobOfferCommandResponse>();

            result.Uri.ShouldNotBeNull();
        }

        [Fact]
        public void Handle_InvalidCompanyId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new CreateJobOfferCommandHandler(_mapper, _logger.Object, _mockJobOfferRepository.Object, _mockCompanyRepository.Object, _mockUriService.Object);

            var command = new CreateJobOfferCommand() { CompanyId = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }
    }
}