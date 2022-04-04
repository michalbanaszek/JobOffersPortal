using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteJobOfferSkillCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
