using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferSkills.Queries.GetJobOfferSkill
{
    public class DeleteOfferSkillQuery : IRequest<JobOfferSkillViewModel>
    {
        public string Id { get; set; }
    }

    public class DeleteOfferSkillQueryHandler : IRequestHandler<DeleteOfferSkillQuery, JobOfferSkillViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOfferSkillQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public DeleteOfferSkillQueryHandler(IMapper mapper, ILogger<DeleteOfferSkillQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<JobOfferSkillViewModel> Handle(DeleteOfferSkillQuery request, CancellationToken cancellationToken)
        {

            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entity = await _context.JobOfferSkills
                                       .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entity == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferSkillViewModel>(entity);
        }
    }
}
