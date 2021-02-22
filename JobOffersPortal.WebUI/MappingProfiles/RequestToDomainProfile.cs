using AutoMapper;
using JobOffersPortal.Contracts.Contracts.Queries;
using JobOffersPortal.WebUI.Contracts.Requests;
using JobOffersPortal.WebUI.Domain;
using System.Linq;

namespace JobOffersPortal.WebUI.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllByFilterQuery, GetAllFilter>();

            CreateMap<CreateCompanyRequest, Company>().ForMember(dest =>
                dest.JobOffers, opt => opt.MapFrom(src => src.JobOffers.Select(x => new CompanyJobOffer() { JobOfferId = x }).ToList()));
            CreateMap<UpdateCompanyRequest, Company>();

            CreateMap<CreateJobOfferRequest, JobOffer>();
            CreateMap<UpdateJobOfferRequest, JobOffer>();
        }
    }
}
