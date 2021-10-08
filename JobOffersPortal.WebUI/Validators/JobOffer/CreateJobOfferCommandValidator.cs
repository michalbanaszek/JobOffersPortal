using Application.JobOffers.Commands.CreateJobOffer;
using FluentValidation;

namespace JobOffersPortal.WebUI.Validators.JobOffer
{
    public class CreateJobOfferCommandValidator : AbstractValidator<CreateJobOfferCommand>
    {
        public CreateJobOfferCommandValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Salary)
                .NotEmpty()
                .NotNull();               

            RuleFor(x => x.Requirements)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Skills)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Propositions)
                .NotEmpty()
                .NotNull();
        }
    }
}
