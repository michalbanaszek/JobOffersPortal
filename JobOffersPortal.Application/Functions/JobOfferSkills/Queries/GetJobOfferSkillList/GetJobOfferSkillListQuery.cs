using MediatR;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList
{
    public class GetJobOfferSkillListQuery : IRequest<List<JobOfferSkillViewModel>>
    {
    }
}
