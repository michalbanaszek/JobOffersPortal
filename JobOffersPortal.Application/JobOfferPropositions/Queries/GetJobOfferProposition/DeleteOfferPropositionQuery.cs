using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferPropositions.Queries.GetJobOfferProposition
{
    public class DeleteOfferPropositionQuery : IRequest<JobOfferPropositionViewModel>
    {
        public string Id { get; set; }
    }

    public class DeleteOfferPropositionQueryHandler : IRequestHandler<DeleteOfferPropositionQuery, JobOfferPropositionViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOfferPropositionQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public DeleteOfferPropositionQueryHandler(IMapper mapper, ILogger<DeleteOfferPropositionQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<JobOfferPropositionViewModel> Handle(DeleteOfferPropositionQuery request, CancellationToken cancellationToken)
        {

            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entity = await _context.JobOfferPropositions
                                       .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferPropositionViewModel>(entity);
        }
    }
}
