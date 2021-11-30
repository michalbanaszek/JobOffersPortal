using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionDetail
{
    public class GetJobOfferPropositionDetailQuery : IRequest<JobOfferPropositionDetailViewModel>
    {
        public string Id { get; set; }
    }
}
