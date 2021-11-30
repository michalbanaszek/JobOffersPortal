using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail
{
    public class GetJobOfferSkillDetailQuery : IRequest<JobOfferSkillDetailViewModel>
    {
        public string Id { get; set; }
    }
}
