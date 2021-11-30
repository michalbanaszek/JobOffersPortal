using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommandValidator : AbstractValidator<UpdateJobOfferRequirementCommand>
    {
        public UpdateJobOfferRequirementCommandValidator()
        {
            RuleFor(x => x.Content)
           .NotEmpty()
           .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
