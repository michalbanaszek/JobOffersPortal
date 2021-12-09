using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteOfferSkillCommand : IRequest<DeleteOfferSkillCommandResponse>
    {
        public string Id { get; set; }
    }
}
