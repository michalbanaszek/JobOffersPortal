using JobOffersPortal.Application.Common.Models;
using JobOffersPortal.Application.Common.Options;
using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers
{
    public class GetCompaniesWithJobOffersListWithPaginationQuery : IRequest<PaginatedList<CompanyJobOfferListViewModel>>
    {
        public SearchJobOfferOptions SearchJobOffer { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
