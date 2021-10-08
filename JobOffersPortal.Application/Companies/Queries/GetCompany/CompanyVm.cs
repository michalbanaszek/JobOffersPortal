using Application.Common.Mappings;
using Application.JobOffers.Queries.GetJobOffer;
using AutoMapper;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Companies.Queries.GetCompany
{
    public class CompanyVm : IMapFrom<Company>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IList<JobOfferViewModel> JobOffers { get; set; } = new List<JobOfferViewModel>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Company, CompanyVm>();
        }
    }
}
