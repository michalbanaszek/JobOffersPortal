using Domain.Entities;
using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommandValidator : AbstractValidator<JobOfferProposition>
    {
        public UpdateJobOfferPropositionCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
