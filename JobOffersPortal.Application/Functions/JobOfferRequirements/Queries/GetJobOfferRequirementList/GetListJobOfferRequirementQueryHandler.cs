using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList
{
    public class GetListJobOfferRequirementQueryHandler : IRequestHandler<GetJobOfferRequirementListQuery, List<JobOfferRequirementViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetListJobOfferRequirementQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetListJobOfferRequirementQueryHandler(IMapper mapper, ILogger<GetListJobOfferRequirementQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<List<JobOfferRequirementViewModel>> Handle(GetJobOfferRequirementListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferRequirements.ToListAsync();

            return _mapper.Map<List<JobOfferRequirementViewModel>>(entity);
        }
    }
}
