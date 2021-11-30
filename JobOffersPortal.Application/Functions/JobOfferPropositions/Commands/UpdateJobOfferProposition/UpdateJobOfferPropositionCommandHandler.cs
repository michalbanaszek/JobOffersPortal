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

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommandHandler : IRequestHandler<UpdateJobOfferPropositionCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateJobOfferPropositionCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public UpdateJobOfferPropositionCommandHandler(IMapper mapper, ILogger<UpdateJobOfferPropositionCommandHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(UpdateJobOfferPropositionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                JobOfferProposition entity = new JobOfferProposition()
                {
                    Id = request.Id,
                    Content = request.Content
                };

                _context.JobOfferPropositions.Update(entity);

                await _context.SaveChangesAsync(new CancellationToken());

                _logger.LogInformation("UpdateJobOfferPropositionCommand execuded.");

                return Unit.Value;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.JobOfferPropositions.Any(e => e.Id == request.Id))
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - NotFoundException execuded.");

                    throw new NotFoundException();
                }
                else
                {
                    _logger.LogWarning("UpdateJobOfferPropositionCommand - Exception execuded.");

                    throw;
                }
            }
        }
    }
}
