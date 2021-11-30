using MediatR;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList
{
    public class GetJobOfferRequirementListQuery : IRequest<List<JobOfferRequirementViewModel>>
    {
    }
}