using MediatR;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public sealed class UpdateJobOfferSkillCommand : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Content { get; set; }
    }
}
