using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail
{
    public class GetJobOfferSkillDetailQueryHandler : IRequestHandler<GetJobOfferSkillDetailQuery, JobOfferSkillDetailViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferSkillDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<JobOfferSkillDetailViewModel> Handle(GetJobOfferSkillDetailQuery request, CancellationToken cancellationToken)
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

            return _mapper.Map<JobOfferSkillDetailViewModel>(entities);
        }
    }
}
