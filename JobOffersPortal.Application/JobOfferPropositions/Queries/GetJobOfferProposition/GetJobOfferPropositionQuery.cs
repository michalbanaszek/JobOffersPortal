using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOfferPropositions.Queries.GetJobOfferProposition
{
    public class GetJobOfferPropositionQuery : IRequest<UpdateJobOfferPropositionViewModel>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferPropositionQueryHandler : IRequestHandler<GetJobOfferPropositionQuery, UpdateJobOfferPropositionViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetJobOfferPropositionQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UpdateJobOfferPropositionViewModel> Handle(GetJobOfferPropositionQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException();
            }

            var entities = await _context.JobOfferPropositions.FirstOrDefaultAsync(m => m.Id == request.Id);

            if (entities == null)
            {
                throw new NotFoundException();
            }

            return _mapper.Map<UpdateJobOfferPropositionViewModel>(entities);
        }
    }
}
