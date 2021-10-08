using Application.JobOffers.Commands.UpdateJobOffer;
using FluentValidation;

namespace JobOffersPortal.WebUI.Validators.JobOffer
{
    public class UpdateJobOfferCommandValidator : AbstractValidator<UpdateJobOfferCommand>
    {
        public UpdateJobOfferCommandValidator()
        {
            RuleFor(x => x.Position)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Salary)
                .NotEmpty()
                .NotNull();             
        }
    }
}
