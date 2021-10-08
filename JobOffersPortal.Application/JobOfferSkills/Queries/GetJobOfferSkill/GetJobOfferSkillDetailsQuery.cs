using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.JobOfferSkills.Command.CreateJobOfferSkill;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferSkills.Queries.GetJobOfferSkill
{
    public class GetJobOfferSkillDetailsQuery : IRequest<CreateDetailsJobOfferSkillViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferSkillDetailsQueryHandler : IRequestHandler<GetJobOfferSkillDetailsQuery, CreateDetailsJobOfferSkillViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferSkillDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateDetailsJobOfferSkillViewModel> Handle(GetJobOfferSkillDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOffers.Include(x => x.Skills)
                                                   .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<CreateDetailsJobOfferSkillViewModel>(entities);
        }
    }
}
