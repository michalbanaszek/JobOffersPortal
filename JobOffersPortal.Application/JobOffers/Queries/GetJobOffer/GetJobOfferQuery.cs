using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.JobOffers.Queries.GetJobOffer
{
    public class GetJobOfferQuery : IRequest<JobOfferVm>
    {
        public string Id { get; set; }
    }

    public class GetJobOfferQueryHandler : IRequestHandler<GetJobOfferQuery, JobOfferVm>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetJobOfferQueryHandler(IMapper mapper, ILogger<GetJobOfferQueryHandler> logger, IApplicationDbContext context)
        {         
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<JobOfferVm> Handle(GetJobOfferQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobOffers
                                       .Include(x => x.Requirements)
                                       .Include(x => x.Skills)
                                       .Include(x => x.Propositions)
                                       .SingleOrDefaultAsync(x => x.Id == request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Get JobOffer Id: {0}", request.Id);

                throw new NotFoundException();
            }

            return _mapper.Map<JobOfferVm>(entity);
        }
    }
}
