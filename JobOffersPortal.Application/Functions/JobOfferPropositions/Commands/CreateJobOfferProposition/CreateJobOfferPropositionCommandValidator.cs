using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandValidator : AbstractValidator<CreateJobOfferPropositionCommand>
    {
        public CreateJobOfferPropositionCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(50)
                .WithMessage("Content Length is between 2 and 50")
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
