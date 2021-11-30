using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementCommandValidator : AbstractValidator<CreateJobOfferRequirementCommand>
    {
        public CreateJobOfferRequirementCommandValidator()
        {
            RuleFor(x => x.Content)
           .NotEmpty()
           .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
