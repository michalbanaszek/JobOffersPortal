using FluentValidation;
using JobOffersPortal.Domain.Entities;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandValidator : AbstractValidator<JobOfferProposition>
    {
        public CreateJobOfferPropositionCommandValidator()
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
