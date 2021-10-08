using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.JobOfferSkills.Command.UpdateJobOfferSkill;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferSkills.Queries.GetJobOfferSkill
{
    public class GetJobOfferSkillQuery : IRequest<UpdateJobOfferSkillViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferSkillQueryHandler : IRequestHandler<GetJobOfferSkillQuery, UpdateJobOfferSkillViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferSkillQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateJobOfferSkillViewModel> Handle(GetJobOfferSkillQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOfferSkills.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<UpdateJobOfferSkillViewModel>(entities);
        }
    }
}
