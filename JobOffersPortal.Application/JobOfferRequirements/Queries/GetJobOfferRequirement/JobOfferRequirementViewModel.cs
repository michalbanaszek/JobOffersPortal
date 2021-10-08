using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.JobOfferRequirements.Queries.GetJobOfferRequirement
{
    public class JobOfferRequirementViewModel : IMapFrom<JobOfferRequirement>
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferRequirement, JobOfferRequirementViewModel>().ReverseMap();
        }
    }
}
