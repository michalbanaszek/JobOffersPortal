using AutoMapper;
using JobOffersPortal.Contracts.Contracts.Responses;
using JobOffersPortal.WebUI.Contracts.Responses;
using JobOffersPortal.WebUI.Domain;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace JobOffersPortal.WebUI.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Company, CompanyResponse>()                
                .ForMember(dest => dest.JobOffers, opt =>
                    opt.MapFrom(src => src.JobOffers.Select(x => new CompanyJobOfferResponse() 
                    {
                        Id = x.JobOfferId                    
                    })));

            CreateMap<JobOffer, JobOfferResponse>();
        }
    }
}
