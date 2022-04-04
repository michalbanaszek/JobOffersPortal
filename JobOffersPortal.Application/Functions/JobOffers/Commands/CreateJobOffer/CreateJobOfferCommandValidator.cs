﻿using Application.JobOffers.Commands.CreateJobOffer;
using FluentValidation;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
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
                .NotNull()
                .MinimumLength(2).MaximumLength(30)
                .WithMessage("Position Length is between 2 and 30")
                .Matches("^[a-zA-Z0-9 ]*$");

            RuleFor(x => x.Salary);             

            RuleFor(x => x.Date)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Requirements)              
                .NotNull();

            RuleFor(x => x.Skills)              
                .NotNull();

            RuleFor(x => x.Propositions)             
                .NotNull();
        }
    }
}
