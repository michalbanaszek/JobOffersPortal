using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList
{
    public class GetJobOfferRequirementListQuery : IRequest<JobOfferRequirementViewModel>
    {
        public string JobOfferId { get; set; }
    }
}