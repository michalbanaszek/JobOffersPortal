using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany;
using JobOffersPortal.Application.UnitTest.Mocks.MockRepositories;
using JobOffersPortal.Application.UnitTest.Mocks.MockServices;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
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

        public DeleteCompanyHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<DeleteCompanyCommandHandler>>();
        }

        [Fact]
        public async Task Handle_ValidCompany_DeletedFromCompanyRepository()
        {
            //Arrange
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new DeleteCompanyCommand() { Id = "1" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            //Assert
            allCompanies.Count.ShouldBe(allCompaniesBeforeCount - 1);

            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidCompanyId_ReturnsNotFoundException()
        {
            //Arrange
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteCompanyCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<NotFoundException>(() => func.Invoke());
        }

        [Fact]
        public void Handle_NotOwnerUser_ReturnsForbiddenAccessException()
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger.Object, _mockCurrentUserService.Object);

            var command = new DeleteCompanyCommand() { Id = "1" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.ThrowsAsync<ForbiddenAccessException>(() => func.Invoke());
        }
    }
}