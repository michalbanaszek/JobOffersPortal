using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Functions.Companies.Commands
{
    public class DeleteCompanyHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<DeleteCompanyCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public DeleteCompanyHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<DeleteCompanyCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCompany_DeletedToCompanyRepo()
        {
            //Arrange
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new DeleteCompanyCommand() { Id = "1" };

            //Act
            await handler.Handle(command, CancellationToken.None);

            //Assert
            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount - 1);
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidCompanyId_NotDeletedToCompanyRepo()
        {
            //Arrange
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteCompanyCommand() { Id = "99" };

            NotFoundException exceptionResponse = null;

            //Act
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException exception)
            {
                exceptionResponse = exception;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"Company\" (99) was not found.");
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotDeletedToCompanyRepo()
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteCompanyCommand() { Id = "1" };

            ForbiddenAccessException exceptionResponse = null;

            //Act
            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (ForbiddenAccessException exception)
            {
                exceptionResponse = exception;
            }

            //Assert
            exceptionResponse.ShouldNotBeNull();

            exceptionResponse.Message.ShouldBe("Entity \"Company\" (1) do not own this entity.");
        }
    }
}