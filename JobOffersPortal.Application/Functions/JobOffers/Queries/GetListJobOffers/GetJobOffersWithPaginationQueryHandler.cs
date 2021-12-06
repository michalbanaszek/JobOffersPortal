﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobOffersPortal.Application.Common.Exceptions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers
{
    public class GetJobOffersWithPaginationQueryHandler : IRequestHandler<GetJobOffersWithPaginationQuery, PaginatedList<JobOfferViewModel>>
    {
        private readonly IJobOfferRepository _jobOfferRepository;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public GetJobOffersWithPaginationQueryHandler(IMapper mapper, IJobOfferRepository jobOfferRepository, IUriService uriService)
        {
            _mapper = mapper;
            _jobOfferRepository = jobOfferRepository;
            _uriService = uriService;
        }

        public async Task<PaginatedList<JobOfferViewModel>> Handle(GetJobOffersWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var companies = _jobOfferRepository.GetAllByCategory(request.CompanyId);         

            if (companies.Count() == 0 && request.CompanyId == null)
            {
                throw new NotFoundException(nameof(Company), request.CompanyId);
            }

            var paginatedEntities = await companies.ProjectTo<JobOfferViewModel>(_mapper.ConfigurationProvider)
                                                  .PaginatedListAsync(request.PageNumber, request.PageSize, _uriService);

            return paginatedEntities;
        }
    }
}