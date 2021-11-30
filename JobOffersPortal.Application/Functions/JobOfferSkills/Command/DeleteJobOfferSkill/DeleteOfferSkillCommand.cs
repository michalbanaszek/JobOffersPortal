using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteOfferSkillCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
