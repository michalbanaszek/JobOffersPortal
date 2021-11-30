using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using MediatR;

namespace JobOffersPortal.Application.Functions.JobOffers.Queries.GetJobOfferDetail
{
    public class GetJobOfferDetailQuery : IRequest<JobOfferViewModel>
    {
        public string Id { get; set; }
    }
}
