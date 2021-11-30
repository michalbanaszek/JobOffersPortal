using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer
{
    public class DeleteJobOfferCommandHandler : IRequestHandler<DeleteJobOfferCommand>
    {
        private readonly IJobOfferService _jobOfferService;
        private readonly ILogger<DeleteJobOfferCommandHandler> _logger;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteJobOfferCommandHandler(IJobOfferService jobOfferService, ILogger<DeleteJobOfferCommandHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _jobOfferService = jobOfferService;
            _logger = logger;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOffers.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobOffer), request.Id);
            }

            var userOwns = await _jobOfferService.UserOwnsEntityAsync(request.Id, _currentUserService.UserId);

            if (!userOwns)
            {
                _logger.LogWarning("Delete company failed - NotFoundUserOwnException, Id: {0}, UserId: {1}", request.Id, _currentUserService.UserId);

                throw new NotFoundUserOwnException(nameof(Company), _currentUserService.UserId);
            }

            _context.JobOffers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Deleted JobOffer Id: {0}", entity.Id);

            return Unit.Value;
        }
    }
}
