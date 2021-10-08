using Application.Common.Mappings;
using Application.JobOffers.Queries.GetJobOffer;
using AutoMapper;
using Domain.Entities;

namespace Application.JobOfferPropositions.Queries.GetJobOfferProposition
{
    public class JobOfferPropositionViewModel : IMapFrom<JobOfferProposition>
    {
        public string Id { get; set; }
        public string Content { get; set; }     

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferProposition, JobOfferPropositionViewModel>().ReverseMap();
        }
    }
}
