using AutoMapper;
using JobOffersPortal.UI.ClientServices;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.DeleteJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferMvc.UpdateJobOfferMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.CreateDetailsPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.DeleteJobOfferPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.DetailJobOfferPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.IndexJobOfferPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferPropositionMvc.UpdateJobOfferPropositionMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.DeleteJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.DetailJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.IndexJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferRequirementMvc.UpdateJobOfferRequirementMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.CreateDetailsJobOfferSkillMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.DeleteJobOfferSkillMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.DetailJobOfferSkillMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.IndexJobOfferSkillMvc;
using JobOffersPortal.UI.ViewModels.JobOfferSkillMvc.UpdateJobOfferSkillMvc;

namespace JobOffersPortal.UI.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // -- JobOffer --

            // Index          
            CreateMap<JobOfferViewModel, JobOfferMvcViewModel>();
            CreateMap<JobOfferRequirementDto, JobOfferRequirementMvcDto>();
            CreateMap<JobOfferSkillDto, JobOfferSkillMvcDto>();
            CreateMap<JobOfferPropositionDto, JobOfferPropositionMvcDto>();

            // Create
            CreateMap<CreateJobOfferMvcViewModel, CreateJobOfferCommand>();

            // Update
            CreateMap<UpdateJobOfferMvcViewModel, UpdateJobOfferCommand>();
            CreateMap<JobOfferMvcViewModel, UpdateJobOfferMvcViewModel>();

            // Delete
            CreateMap<JobOfferMvcViewModel, JobOfferDeleteMvcViewModel>();


            // -- JobOfferSkill --

            // Index
            CreateMap<JobOfferSkillViewModel, JobOfferSkillMvcViewModel>();

            // Detail
            CreateMap<JobOfferSkillDetailViewModel, JobOfferSkillDetailMvcViewModel>();

            // Edit
            CreateMap<JobOfferSkillDetailMvcViewModel, UpdateJobOfferSkillMvcViewModel>();

            //CreateShowDetails         
            CreateMap<JobOfferSkillMvcDto, JobOfferJobOfferSkillMvcDto>();

            // Delete
            CreateMap<JobOfferSkillDetailMvcViewModel, JobOfferSkillDeleteMvcViewModel>();


            // -- JobOfferProposition --

            // Index
            CreateMap<JobOfferPropositionViewModel, JobOfferPropositionMvcViewModel>();

            // Detail
            CreateMap<JobOfferPropositionDetailViewModel, JobOfferPropositionDetailMvcViewModel>();

            // Edit
            CreateMap<JobOfferPropositionDetailMvcViewModel, UpdateJobOfferPropositionMvcViewModel>();

            //CreateShowDetails         
            CreateMap<JobOfferPropositionMvcDto, JobOfferJobOfferPropositionMvcDto>();

            // Delete
            CreateMap<JobOfferPropositionDetailMvcViewModel, DeleteJobOfferPropositionMvcViewModel>();


            // -- JobOfferRequirement --

            // Index
            CreateMap<JobOfferRequirementViewModel, JobOfferRequirementMvcViewModel>();

            // Detail
            CreateMap<JobOfferRequirementDetailViewModel, JobOfferRequirementDetailMvcViewModel>();

            // Edit
            CreateMap<JobOfferRequirementDetailMvcViewModel, UpdateJobOfferRequirementMvcViewModel>();

            //CreateShowDetails         
            CreateMap<JobOfferRequirementMvcDto, JobOfferJobOfferRequirementMvcDto>();

            // Delete
            CreateMap<JobOfferRequirementDetailMvcViewModel, JobOfferRequirementDeleteMvcViewModel>();
        }
    }
}