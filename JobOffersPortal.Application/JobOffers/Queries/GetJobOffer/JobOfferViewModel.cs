using Application.Common.Mappings;
using Application.JobOfferPropositions.Queries.GetJobOfferProposition;
using Application.JobOfferRequirements.Queries.GetJobOfferRequirement;
using Application.JobOfferSkills.Queries.GetJobOfferSkill;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.JobOffers.Queries.GetJobOffer
{
    public class JobOfferViewModel  : IMapFrom<JobOffer>
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public List<JobOfferRequirementViewModel> Requirements { get; set; } = new List<JobOfferRequirementViewModel>();

        public List<JobOfferSkillViewModel> Skills { get; set; } = new List<JobOfferSkillViewModel>();

        public List<JobOfferPropositionViewModel> Propositions { get; set; } = new List<JobOfferPropositionViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOffer, JobOfferViewModel>().ReverseMap();          
        }
    }
}
