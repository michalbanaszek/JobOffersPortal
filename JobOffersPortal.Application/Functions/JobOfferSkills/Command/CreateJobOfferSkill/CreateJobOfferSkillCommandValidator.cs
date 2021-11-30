using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommandValidator : AbstractValidator<CreateJobOfferSkillCommand>
    {
        public CreateJobOfferSkillCommandValidator()
        {
            RuleFor(x => x.Content)
           .NotEmpty()
           .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
