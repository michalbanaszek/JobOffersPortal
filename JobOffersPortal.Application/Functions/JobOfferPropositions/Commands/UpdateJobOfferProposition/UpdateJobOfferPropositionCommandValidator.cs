using FluentValidation;
using JobOffersPortal.Domain.Entities;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommandValidator : AbstractValidator<UpdateJobOfferPropositionCommand>
    {
        public UpdateJobOfferPropositionCommandValidator()
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
