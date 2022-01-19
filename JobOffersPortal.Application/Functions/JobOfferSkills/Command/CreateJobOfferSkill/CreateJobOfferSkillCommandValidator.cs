using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommandValidator : AbstractValidator<CreateJobOfferSkillCommand>
    {
        public CreateJobOfferSkillCommandValidator()
        {
            RuleFor(x => x.Content)
               .NotEmpty()
               .NotNull()
               .MinimumLength(2).MaximumLength(50)
               .WithMessage("{PropertName} Length is between 2 and 50")
               .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
