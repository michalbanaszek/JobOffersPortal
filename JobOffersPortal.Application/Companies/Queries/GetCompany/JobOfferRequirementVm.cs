using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.Companies.Queries.GetCompany
{
    public class JobOfferRequirementVm : IMapFrom<JobOfferRequirement>
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferRequirement, JobOfferRequirementVm>();
        }
    }
}
