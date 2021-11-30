using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionDetail
{
    public class GetJobOfferPropositionDetailQueryHandler : IRequestHandler<GetJobOfferPropositionDetailQuery, JobOfferPropositionDetailViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferPropositionDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<JobOfferPropositionDetailViewModel> Handle(GetJobOfferPropositionDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOfferPropositions.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferPropositionDetailViewModel>(entities);
        }
    }
}
