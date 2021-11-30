using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList
{
    public class GetJobOfferPropositionListQueryHandler : IRequestHandler<GetJobOfferPropositionListQuery, List<JobOfferPropositionViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferPropositionListQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetJobOfferPropositionListQueryHandler(IMapper mapper, ILogger<GetJobOfferPropositionListQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<List<JobOfferPropositionViewModel>> Handle(GetJobOfferPropositionListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferPropositions.ToListAsync();

            return _mapper.Map<List<JobOfferPropositionViewModel>>(entity);
        }
    }
}
