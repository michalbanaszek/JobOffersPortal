using FluentValidation;
using JobOffersPortal.Application.Common.Interfaces.Persistance;
using System;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandValidator : AbstractValidator<UpdateJobOfferCommand>
    {
        public UpdateJobOfferCommandValidator(IJobOfferRepository jobOfferRepository)
        {
            RuleFor(x => x.Position)              
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Position Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");

            RuleFor(x => x.Date)
               .NotEmpty()
               .NotNull()
               .LessThan(DateTime.Now.AddDays(1));
        }
    }
}
