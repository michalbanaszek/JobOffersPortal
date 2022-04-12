using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail;
using MediatR;

namespace Application.JobOfferRequirements.Queries.GetJobOfferRequirement
{
    public class GetJobOfferRequirementDetailQuery : IRequest<JobOfferRequirementDetailViewModel>
    {
        public string Id { get; set; }
    }
}
