using Domain.Entities;
using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandValidator : AbstractValidator<JobOfferProposition>
    {
        public CreateJobOfferPropositionCommandValidator()
        {
            RuleFor(x => x.Content)
             .NotEmpty()
             .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
