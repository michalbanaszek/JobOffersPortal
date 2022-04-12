using Application.JobOffers.Commands.CreateJobOffer;
using AutoMapper;
using JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany;
using JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyDetail;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyList;
using JobOffersPortal.Application.Functions.Companies.Queries.GetCompanyListWithJobOffers;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.CreateJobOfferProposition;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionDetail;
using JobOffersPortal.Application.Functions.JobOfferPropositions.Queries.GetJobOfferPropositionList;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementDetail;
using JobOffersPortal.Application.Functions.JobOfferRequirements.Queries.GetJobOfferRequirementList;
using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using JobOffersPortal.Application.Functions.JobOffers.Queries.GetListJobOffers;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill;
using JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill;
using JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillDetail;
using JobOffersPortal.Application.Functions.JobOfferSkills.Queries.GetJobOfferSkillList;
using JobOffersPortal.Domain.Entities;
using System;
using System.Linq;

namespace JobOffersPortal.Application.Common.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Company, CompanyDetailViewModel>();
            CreateMap<JobOffer, JobOfferDto>();

            CreateMap<Company, CompanyJobOfferListViewModel>();
            CreateMap<JobOfferProposition, JobOfferPropositionDto>();
            CreateMap<JobOfferRequirement, JobOfferRequirementDto>();
            CreateMap<JobOfferSkill, JobOfferSkillDto>();
            CreateMap<JobOffer, JobOfferViewModel>().ReverseMap();

            CreateMap<JobOffer, JobOfferWithRequirementWithSkillWithPropositionDto>();

            CreateMap<CreateCompanyCommand, Company>();
            CreateMap<UpdateCompanyCommand, Company>();

            CreateMap<JobOfferProposition, JobOfferPropositionDetailViewModel>().ReverseMap();

            CreateMap<JobOfferProposition, JobOfferPropositionViewModel>().ReverseMap();

            CreateMap<CreateJobOfferPropositionCommand, CreateJobOfferPropositionCommandResponse>();

            CreateMap<JobOfferRequirement, JobOfferRequirementDetailViewModel>().ReverseMap();

            CreateMap<CreateJobOfferCommand, JobOffer>()
                   .ForMember(dest => dest.Requirements,
                      opt => opt.MapFrom(src => src.Requirements.Select(x =>
                      new JobOfferRequirement() { Id = Guid.NewGuid().ToString(), Content = x })))
                   .ForMember(dest => dest.Skills,
                      opt => opt.MapFrom(src => src.Skills.Select(y =>
                      new JobOfferSkill() { Id = Guid.NewGuid().ToString(), Content = y })))
                   .ForMember(dest => dest.Propositions,
                      opt => opt.MapFrom(src => src.Propositions.Select(z =>
                      new JobOfferProposition() { Id = Guid.NewGuid().ToString(), Content = z })));

            CreateMap<UpdateJobOfferCommand, JobOffer>();

            CreateMap<CreateJobOfferSkillCommand, CreateJobOfferSkillCommandResponse>();

            CreateMap<JobOfferSkill, JobOfferSkillDetailViewModel>();

            CreateMap<JobOffer, JobOfferWithRequirementWithSkillWithPropositionDto>();
            CreateMap<JobOfferProposition, JobOfferWithPropositionDto>();
            CreateMap<JobOfferRequirement, JobOfferWithRequirementDto>();
            CreateMap<JobOfferSkill, JobOfferWithSkillDto>();


            CreateMap<JobOffer, CreateJobOfferSkillCommandResponse>();
            CreateMap<JobOffer, JobOfferSkillViewModel>();
            CreateMap<JobOfferSkill, JobOfferJobOfferSkillDto>();
            CreateMap<UpdateJobOfferSkillCommand, JobOfferSkill>();

            CreateMap<JobOffer, CreateJobOfferRequirementCommandResponse>();
            CreateMap<JobOffer, JobOfferRequirementViewModel>();
            CreateMap<UpdateJobOfferRequirementCommand, JobOfferRequirement>();
            CreateMap<JobOfferRequirement, JobOfferJobOfferRequirementDto>();

            CreateMap<JobOffer, CreateJobOfferPropositionCommandResponse>();
            CreateMap<JobOffer, JobOfferPropositionViewModel>();
            CreateMap<UpdateJobOfferPropositionCommand, JobOfferProposition>();
            CreateMap<JobOfferProposition, JobOfferJobOfferPropositionDto>();
        }
    }
}
