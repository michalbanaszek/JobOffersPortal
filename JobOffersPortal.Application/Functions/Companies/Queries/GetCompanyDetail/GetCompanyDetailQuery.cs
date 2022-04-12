using MediatR;

namespace JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail
{
    public class GetCompanyDetailQuery : IRequest<CompanyDetailViewModel>
    {
        public string Id { get; set; }
    }
}
