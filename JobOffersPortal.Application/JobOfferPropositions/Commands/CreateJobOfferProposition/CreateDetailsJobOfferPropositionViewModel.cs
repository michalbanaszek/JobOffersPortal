using Application.Common.Mappings;
using Application.JobOfferPropositions.Queries.GetJobOfferProposition;
using Application.JobOffers.Queries.GetJobOffer;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateDetailsJobOfferPropositionViewModel : IMapFrom<JobOfferViewModel>
    {
        public string Id { get; set; }

        public string CompanyId { get; set; }

        public string Position { get; set; }

        public string Salary { get; set; }

        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }

        public string Content { get; set; }

     
        public List<JobOfferPropositionViewModel> Propositions { get; set; } = new List<JobOfferPropositionViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferViewModel, CreateDetailsJobOfferPropositionViewModel>();
            profile.CreateMap<JobOffer, CreateDetailsJobOfferPropositionViewModel>();
        }
    }
}
