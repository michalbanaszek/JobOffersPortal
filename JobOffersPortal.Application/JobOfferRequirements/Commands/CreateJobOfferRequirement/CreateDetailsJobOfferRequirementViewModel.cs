using Application.Common.Mappings;
using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using Application.JobOffers.Queries.GetJobOffer;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateDetailsJobOfferRequirementViewModel : IMapFrom<JobOfferViewModel>
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public string Content { get; set; }


        public List<JobOfferRequirementViewModel> Requirements { get; set; } = new List<JobOfferRequirementViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferViewModel, CreateDetailsJobOfferRequirementViewModel>();
            profile.CreateMap<JobOffer, CreateDetailsJobOfferRequirementViewModel>();
        }
    }
}
