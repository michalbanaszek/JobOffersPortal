using Application.Common.Mappings;
using Application.Companies.Queries.GetCompany;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.JobOffers.Queries.GetJobOffer
{
    public class JobOfferVm  : IMapFrom<JobOffer>
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

        public DateTime Date { get; set; }

        public List<JobOfferRequirementVm> Requirements { get; set; } = new List<JobOfferRequirementVm>();

        public List<JobOfferSkillVm> Skills { get; set; } = new List<JobOfferSkillVm>();

        public List<JobOfferPropositionVm> Propositions { get; set; } = new List<JobOfferPropositionVm>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOffer, JobOfferVm>();
        }
    }
}
