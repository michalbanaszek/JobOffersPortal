﻿using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace JobOffersPortal.Application.UnitTest.Companies.Commands
{
    public class DeleteCompanyTest : BaseCompanyInitialization
    {
        private readonly ILogger<DeleteCompanyCommandHandler> _logger;

        public DeleteCompanyTest()
        {
            _logger = (new Mock<ILogger<DeleteCompanyCommandHandler>>()).Object;
        }

        [Fact]
        public async Task Handle_ValidCompany_DeletedToCompanyRepo()
        {
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger, _currentUserService);

            var allCompaniesBeforeCount = (await _mockCompanyRepository.Object.GetAllAsync()).Count;

            var command = new DeleteCompanyCommand() { Id = "1" };

            await handler.Handle(command, CancellationToken.None);

            var allCompanies = await _mockCompanyRepository.Object.GetAllAsync();

            allCompanies.Count.ShouldBe(allCompaniesBeforeCount - 1);
        }

        [Fact]
        public async Task HandleNotFoundException_InvalidCompanyId_NotDeletedToCompanyRepo()
        {
            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger, _currentUserService);

            var command = new DeleteCompanyCommand() { Id = "99" };

            NotFoundException exceptionResponse = null;

            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (NotFoundException exception)
            {
                exceptionResponse = exception;
            }
            finally
            {
                exceptionResponse.ShouldNotBeNull();
                exceptionResponse.Message.ShouldBe("Entity \"Company\" (99) was not found.");
            }
        }

        [Fact]
        public async Task HandleForbiddenAccessException_NotOwnUser_NotDeletedToCompanyRepo()
        {
            _currentUserServiceMock.SetupGet(x => x.UserId).Returns("user2");

            var handler = new DeleteCompanyCommandHandler(_mockCompanyRepository.Object, _logger, _currentUserService);

            var command = new DeleteCompanyCommand() { Id = "1" };

            ForbiddenAccessException exceptionResponse = null;

            try
            {
                await handler.Handle(command, CancellationToken.None);
            }
            catch (ForbiddenAccessException exception)
            {
                exceptionResponse = exception;
            }
            finally
            {
                exceptionResponse.ShouldNotBeNull();
                exceptionResponse.Message.ShouldBe("Entity \"Company\" (1) do not own this entity.");
            }
        }
    }
}