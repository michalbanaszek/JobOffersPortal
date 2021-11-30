using MediatR;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList
{
    public class GetJobOfferPropositionListQuery : IRequest<List<JobOfferPropositionViewModel>>
    {
    }
}
