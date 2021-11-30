using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail
{
    public class GetJobOfferDetailQueryHandler : IRequestHandler<GetJobOfferDetailQuery, JobOfferViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferDetailQueryHandler> _logger;
        private readonly IApplicationDbContext _context;

        public GetJobOfferDetailQueryHandler(IMapper mapper, ILogger<GetJobOfferDetailQueryHandler> logger, IApplicationDbContext context)
        {
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<JobOfferViewModel> Handle(GetJobOfferDetailQuery request, CancellationToken cancellationToken)
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

            return _mapper.Map<JobOfferViewModel>(entity);
        }
    }
}
