﻿using FluentValidation;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.UpdateJobOfferRequirementMvc;

namespace JobOffersPortal.UI.Validators.JobOfferRequirementMvcValidator
{
    public class UpdateJobOfferRequirementMvcViewModelValidator : AbstractValidator<UpdateJobOfferRequirementMvcViewModel>
    {
        public UpdateJobOfferRequirementMvcViewModelValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
        }
    }
}