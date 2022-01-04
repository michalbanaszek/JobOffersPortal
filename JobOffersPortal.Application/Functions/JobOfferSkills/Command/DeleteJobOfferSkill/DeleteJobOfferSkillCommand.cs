using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteJobOfferSkillCommand : IRequest<DeleteJobOfferSkillCommandResponse>
    {
        public string Id { get; set; }
    }
}
