using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteOfferPropositionCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }

    public class DeleteOfferPropositionCommandHandler : IRequestHandler<DeleteOfferPropositionCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOfferPropositionCommandHandler> _logger;
        private readonly IApplicationDbContext _context;

        public DeleteOfferPropositionCommandHandler(IMapper mapper, ILogger<DeleteOfferPropositionCommandHandler> logger, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOfferPropositionCommand request, CancellationToken cancellationToken)
        {
            var jobOfferProposition = await _context.JobOfferPropositions.FindAsync(request.Id);

            _context.JobOfferPropositions.Remove(jobOfferProposition);

            await _context.SaveChangesAsync(new CancellationToken());

            _logger.LogInformation("Deleted JobOfferProposition Id: {0}", request.Id);

            return Unit.Value;           
        }
    }
}
