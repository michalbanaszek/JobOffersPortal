using Application.Common.Interfaces;
using Application.JobOfferPropositions.Queries.GetJobOfferProposition;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferPropositions.Queries.GetListJobOfferProposition
{
    public class GetListJobOfferPropositionQuery : IRequest<List<JobOfferPropositionViewModel>>
    {
    }

    public class GetJobOfferPropositionQueryHandler : IRequestHandler<GetListJobOfferPropositionQuery, List<JobOfferPropositionViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferPropositionQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetJobOfferPropositionQueryHandler(IMapper mapper, ILogger<GetJobOfferPropositionQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<List<JobOfferPropositionViewModel>> Handle(GetListJobOfferPropositionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferPropositions.ToListAsync();

            return _mapper.Map<List<JobOfferPropositionViewModel>>(entity);
        }
    }
}
