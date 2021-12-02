using FluentValidation;
using JobOffersPortal.Domain.Entities;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommandValidator : AbstractValidator<JobOfferProposition>
    {
        public UpdateJobOfferPropositionCommandValidator()
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
