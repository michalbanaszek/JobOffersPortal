using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommand : IRequest<CreateJobOfferSkillCommandResponse>
    {
        public string JobOfferId { get; set; }
        public string Content { get; set; }
    }
}
