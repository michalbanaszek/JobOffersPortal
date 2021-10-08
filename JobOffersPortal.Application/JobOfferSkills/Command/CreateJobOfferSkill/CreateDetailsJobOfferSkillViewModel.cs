using Application.Common.Mappings;
using Application.JobOffers.Queries.GetJobOffer;
using Application.JobOfferSkills.Queries.GetJobOfferSkill;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateDetailsJobOfferSkillViewModel : IMapFrom<JobOfferViewModel>
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public string Content { get; set; }

        public List<JobOfferSkillViewModel> Skills { get; set; } = new List<JobOfferSkillViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferViewModel, CreateDetailsJobOfferSkillViewModel>();
            profile.CreateMap<JobOffer, CreateDetailsJobOfferSkillViewModel>();
        }
    }
}
