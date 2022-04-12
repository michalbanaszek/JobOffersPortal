using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Extensions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompaniesListWithPaginationQueryHandler : IRequestHandler<GetCompaniesListWithPaginationQuery, PaginatedList<CompanyJobOfferListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUriService _uriService;

        public GetCompaniesListWithPaginationQueryHandler(IMapper mapper, IUriService uriService, ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _uriService = uriService;
        }

        public async Task<PaginatedList<CompanyJobOfferListViewModel>> Handle(GetCompaniesListWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var entities = _companyRepository.GetAllCompaniesIncludeEntitiesWithOptions(request.SearchJobOffer);

            var paginatedEntities = await entities.ProjectTo<CompanyJobOfferListViewModel>(_mapper.ConfigurationProvider)
                          .PaginatedListAsync(request.PageNumber, request.PageSize, _uriService);

            return paginatedEntities;
        }
    }
}
