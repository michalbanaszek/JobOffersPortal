using JobOffersPortal.Application.Common.Enums;
using JobOffersPortal.Application.Common.Models;
using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList
{
    public class GetCompaniesListWithPaginationQuery : IRequest<PaginatedList<CompanyJobOfferListViewModel>>
    {
        public SearchCompanyOptions SearchJobOffer { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
