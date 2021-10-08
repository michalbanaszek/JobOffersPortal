using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Queries.GetJobOfferRequirement
{
    public class GetJobOfferRequirementQuery : IRequest<UpdateJobOfferRequirementViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferRequirementQueryHandler : IRequestHandler<GetJobOfferRequirementQuery, UpdateJobOfferRequirementViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferRequirementQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateJobOfferRequirementViewModel> Handle(GetJobOfferRequirementQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOfferRequirements.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<UpdateJobOfferRequirementViewModel>(entities);
        }
    }
}
