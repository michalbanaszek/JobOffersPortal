using Application.Common.Interfaces;
using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Queries.GetListJobOfferRequirement
{
    public class GetListJobOfferRequirementQuery : IRequest<List<JobOfferRequirementViewModel>>
    {
    }

    public class GetListJobOfferRequirementQueryHandler : IRequestHandler<GetListJobOfferRequirementQuery, List<JobOfferRequirementViewModel>>
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

        public async Task<List<JobOfferRequirementViewModel>> Handle(GetListJobOfferRequirementQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferRequirements.ToListAsync();

            return _mapper.Map<List<JobOfferRequirementViewModel>>(entity);
        }
    }
}