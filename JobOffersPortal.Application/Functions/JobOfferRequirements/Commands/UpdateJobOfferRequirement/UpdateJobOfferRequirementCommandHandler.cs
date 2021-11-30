using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommandHandler : IRequestHandler<UpdateJobOfferRequirementCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferRequirementCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public UpdateJobOfferRequirementCommandHandler(IMapper mapper, ILogger<UpdateJobOfferRequirementCommandHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateJobOfferRequirementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JobOfferRequirement entity = new JobOfferRequirement()
                {
                    Id = request.Id,
                    Content = request.Content
                };

                _context.JobOfferRequirements.Update(entity);

                await _context.SaveChangesAsync(new CancellationToken());

                _logger.LogInformation("UpdateJobOfferRequirementCommand execuded.");

                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.JobOfferRequirements.Any(e => e.Id == request.Id))
                {
                    _logger.LogWarning("UpdateJobOfferRequirementCommand - NotFoundException execuded.");

                    throw new NotFoundException();
                }
                else
                {
                    _logger.LogWarning("UpdateJobOfferRequirementCommand - Exception execuded.");

                    throw;
                }
            }
        }
    }
}
