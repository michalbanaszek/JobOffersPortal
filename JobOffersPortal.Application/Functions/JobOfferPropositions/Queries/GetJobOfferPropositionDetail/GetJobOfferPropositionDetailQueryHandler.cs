﻿using AutoMapper;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionDetail
{
    public class GetJobOfferPropositionDetailQueryHandler : IRequestHandler<GetJobOfferPropositionDetailQuery, JobOfferPropositionDetailViewModel>
    {       
        private readonly IMapper _mapper;
        private readonly IJobOfferPropositionRepository _jobOfferPropositionRepository;

        public GetJobOfferPropositionDetailQueryHandler(IJobOfferPropositionRepository jobOfferPropositionRepository, IMapper mapper)
        {
            _jobOfferPropositionRepository = jobOfferPropositionRepository;
            _mapper = mapper;
        }

        public async Task<JobOfferPropositionDetailViewModel> Handle(GetJobOfferPropositionDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException(nameof(JobOfferProposition), request.Id);
            }

            var entities = await _jobOfferPropositionRepository.GetByIdAsync(request.Id);

            if (entities == null)
            {
                throw new NotFoundException(nameof(JobOfferProposition), request.Id);
            }

            return _mapper.Map<JobOfferPropositionDetailViewModel>(entities);         
        }
    }
}
