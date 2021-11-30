using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public class UpdateJobOfferSkillCommandValidator : AbstractValidator<UpdateJobOfferSkillCommand>
    {
        public UpdateJobOfferSkillCommandValidator()
        {
            RuleFor(x => x.Content)
           .NotEmpty()
           .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
