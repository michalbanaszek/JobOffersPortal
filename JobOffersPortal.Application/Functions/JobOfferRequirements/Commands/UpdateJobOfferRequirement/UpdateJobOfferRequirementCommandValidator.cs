using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public class UpdateJobOfferRequirementCommandValidator : AbstractValidator<UpdateJobOfferRequirementCommand>
    {
        public UpdateJobOfferRequirementCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(50)
                .WithMessage("{PropertName} Length is beewten 2 and 50")
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
