using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.JobOfferPropositions.Queries.GetJobOfferProposition
{
    public class UpdateJobOfferPropositionViewModel : IMapFrom<JobOfferProposition>
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferProposition, UpdateJobOfferPropositionViewModel>().ReverseMap();
        }
    }
}
