using Application.Common.Interfaces;
using Application.JobOfferSkills.Queries.GetJobOfferSkill;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferSkills.Queries.GetListJobOfferSkill
{
    public class GetListJobOfferSkillQuery : IRequest<List<JobOfferSkillViewModel>>
    {
    }

    public class GetListJobOfferSkillQueryHandler : IRequestHandler<GetListJobOfferSkillQuery, List<JobOfferSkillViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetListJobOfferSkillQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetListJobOfferSkillQueryHandler(IMapper mapper, ILogger<GetListJobOfferSkillQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<List<JobOfferSkillViewModel>> Handle(GetListJobOfferSkillQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOfferPropositions.ToListAsync();

            return _mapper.Map<List<JobOfferSkillViewModel>>(entity);
        }
    }
}
