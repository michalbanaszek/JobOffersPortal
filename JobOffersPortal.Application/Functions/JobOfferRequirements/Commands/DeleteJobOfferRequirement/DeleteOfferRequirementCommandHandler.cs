﻿using Application.Common.Interfaces;
using Application.JobOfferRequirements.Commands.DeleteJobOfferRequirement;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteOfferRequirementCommandHandler : IRequestHandler<DeleteOfferRequirementCommand, Unit>
    {
        private readonly ILogger<DeleteOfferRequirementCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public DeleteOfferRequirementCommandHandler(ILogger<DeleteOfferRequirementCommandHandler> logger, IApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferRequirements.FindAsync(request.Id);

            _context.JobOfferRequirements.Remove(entity);

            await _context.SaveChangesAsync(new CancellationToken());

            _logger.LogInformation("Deleted JobOfferRequirement Id: {0}", request.Id);

            return Unit.Value;
        }
    }
}
