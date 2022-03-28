using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteJobOfferCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly ILogger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserService userService, ILogger<DeleteUserCommandHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task<DeleteJobOfferCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userResult = await _userService.GetUserByIdAsync(request.Id);

            if (userResult == null)
            {
                _logger.LogWarning("User Id not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException("User", request.Id);
            }

            try
            {
                await _userService.DeleteUserAsync(userResult.Id);

                _logger.LogInformation("Deleted User Id: {0}", userResult.Id);

                return new DeleteJobOfferCommandResponse(userResult.Id);
            }
            catch (DbUpdateConcurrencyException dbUpdateConcurrencyException)
            {
                _logger.LogError("DbUpdateConcurrencyException execuded, Message:", dbUpdateConcurrencyException.Message);

                return new DeleteJobOfferCommandResponse(false, new string[] { "Cannot add user to database." });
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception execuded, Message:", exception.Message);

                return new DeleteJobOfferCommandResponse(false, new string[] { "Cannot add user to database." });
            }
        }
    }
}
