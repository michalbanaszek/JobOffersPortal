using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.JobOfferPropositions.Commands.CreateJobOfferProposition;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferPropositions.Queries.GetJobOfferProposition
{
    public class GetJobOfferPropositionDetailsQuery : IRequest<CreateDetailsJobOfferPropositionViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferPropositionDetailsQueryHandler : IRequestHandler<GetJobOfferPropositionDetailsQuery, CreateDetailsJobOfferPropositionViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferPropositionDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateDetailsJobOfferPropositionViewModel> Handle(GetJobOfferPropositionDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOffers.Include(x => x.Propositions)
                                                   .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<CreateDetailsJobOfferPropositionViewModel>(entities);
        }
    }
}
