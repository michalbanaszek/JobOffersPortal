using AutoMapper;
using AutoMapper.QueryableExtensions;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using JobOffersPortal.Application.Common.Mappings;
using JobOffersPortal.Application.Common.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers
{
    public class GetCompaniesWithJobOffersListWithPaginationQueryHandler : IRequestHandler<GetCompaniesWithJobOffersListWithPaginationQuery, PaginatedList<CompanyJobOfferListViewModel>>
    {      
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;        
        private readonly IUriService _uriService;

        public GetCompaniesWithJobOffersListWithPaginationQueryHandler(IMapper mapper, IUriService uriService, ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _uriService = uriService;


        }

        public async Task<PaginatedList<CompanyJobOfferListViewModel>> Handle(GetCompaniesWithJobOffersListWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var entities = _companyRepository.GetAllCompaniesIncludeEntitiesWithOptions(request.SearchJobOffer);

            var paginatedEntities = await entities.ProjectTo<CompanyJobOfferListViewModel>(_mapper.ConfigurationProvider)
                          .PaginatedListAsync(request.PageNumber, request.PageSize, _uriService);

            return paginatedEntities;

        }
    }
}
