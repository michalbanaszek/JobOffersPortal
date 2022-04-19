using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
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
    public class UpdateCompanyCommandHandlerTests
    {
        private readonly Mock<ICompanyRepository> _mockCompanyRepository;
        private readonly Mock<ICurrentUserService> _mockCurrentUserService;
        private readonly Mock<ILogger<UpdateCompanyCommandHandler>> _logger;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandlerTests()
        {
            _mockCompanyRepository = MockCompanyRepository.GetCompanyRepository();
            _mockCurrentUserService = MockCurrentUserService.GetCurrentUserService();
            _logger = new Mock<ILogger<UpdateCompanyCommandHandler>>();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCompany_UpdatedToCompanyRepository()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "UpdateCompany" };

            //Act
            await handler.Handle(command, CancellationToken.None);

            var entityUpdated = await _mockCompanyRepository.Object.GetByIdAsync(command.Id);

            //Assert
            entityUpdated.Id.ShouldBe("1");

            entityUpdated.Name.ShouldBe("UpdateCompany");
        }

        [Fact]
        public async Task Handle_ValidCompany_ReturnsSpecyficType()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateCompanyCommand() { Id = "1", Name = "UpdateCompany" };

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert   
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public void Handle_InvalidCompanyId_ThrowsNotFoundException()
        {
            //Arrange
            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateCompanyCommand() { Id = "99" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<NotFoundException>();
        }

        [Fact]
        public void Handle_NotOwnerUser_ThrowsForbiddenAccessException()
        {
            //Arrange
            _mockCurrentUserService.SetupGet(x => x.UserId).Returns("user2");

            var handler = new UpdateCompanyCommandHandler(_mockCompanyRepository.Object, _mapper, _logger.Object, _mockCurrentUserService.Object);

            var command = new UpdateCompanyCommand() { Id = "1" };

            //Act
            Func<Task> func = () => handler.Handle(command, CancellationToken.None);

            //Assert
            func.ShouldThrowAsync<ForbiddenAccessException>();
        }
    }
}