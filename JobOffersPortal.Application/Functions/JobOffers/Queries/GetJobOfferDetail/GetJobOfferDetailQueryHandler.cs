using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using JobOffersPortal.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail
{
    public class GetJobOfferDetailQueryHandler : IRequestHandler<GetJobOfferDetailQuery, JobOfferViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetJobOfferDetailQueryHandler> _logger;
        private readonly IJobOfferRepository _jobOfferRepository;

        public GetJobOfferDetailQueryHandler(IMapper mapper, ILogger<GetJobOfferDetailQueryHandler> logger, IJobOfferRepository jobOfferRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _jobOfferRepository = jobOfferRepository;
        }

        public async Task<JobOfferViewModel> Handle(GetJobOfferDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _jobOfferRepository.GetByIdIncludeAllEntities(request.Id);

            if (entity == null)
            {
                _logger.LogWarning("Entity not found from database. Request ID: {0}", request.Id);

                throw new NotFoundException(nameof(JobOffer), request.Id);
            }

            return _mapper.Map<JobOfferViewModel>(entity);
        }
    }
}