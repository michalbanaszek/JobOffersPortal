using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.JobOfferRequirements.Commands.CreateJobOfferRequirement;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferRequirements.Queries.GetJobOfferRequirement
{
    public class GetJobOfferRequirementDetailsQuery : IRequest<CreateDetailsJobOfferRequirementViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferRequirementDetailsQueryHandler : IRequestHandler<GetJobOfferRequirementDetailsQuery, CreateDetailsJobOfferRequirementViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferRequirementDetailsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateDetailsJobOfferRequirementViewModel> Handle(GetJobOfferRequirementDetailsQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOffers.Include(x => x.Requirements)
                                                   .FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<CreateDetailsJobOfferRequirementViewModel>(entities);
        }
    }
}
