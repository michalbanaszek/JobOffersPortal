using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public class UpdateJobOfferSkillCommand : IRequest<UpdateJobOfferSkillCommandResponse>
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
