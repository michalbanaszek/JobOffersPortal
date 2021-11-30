using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList
{
    public class GetJobOfferSkillListQueryHandler : IRequestHandler<GetJobOfferSkillListQuery, List<JobOfferSkillViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferSkillListQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetJobOfferSkillListQueryHandler(IMapper mapper, ILogger<GetJobOfferSkillListQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<List<JobOfferSkillViewModel>> Handle(GetJobOfferSkillListQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferSkills.ToListAsync();

            return _mapper.Map<List<JobOfferSkillViewModel>>(entity);
        }
    }
}
