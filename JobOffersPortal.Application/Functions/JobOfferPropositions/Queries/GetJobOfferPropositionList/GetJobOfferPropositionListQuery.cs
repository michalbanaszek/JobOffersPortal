using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList
{
    public class GetJobOfferPropositionListQuery : IRequest<JobOfferPropositionViewModel>
    {
        public string JobOfferId { get; set; }
    }
}
