using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList
{
    public class GetJobOfferSkillListQuery : IRequest<JobOfferSkillViewModel>
    {
        public string JobOfferId { get; set; }
    }
}
