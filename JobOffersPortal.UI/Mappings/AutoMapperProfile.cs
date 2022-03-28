using AutoMapper;
using JobOffersPortal.UI.ClientServices;
using WebApp.ViewModels.JobOfferMvc.CreateJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.DeleteJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.IndexJobOfferMvc;
using WebApp.ViewModels.JobOfferMvc.UpdateJobOfferMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.CreateDetailsPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.DeleteJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.DetailJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.IndexJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferPropositionMvc.UpdateJobOfferPropositionMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.CreateDetailsJobOfferRequirrementMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.DeleteJobOfferRequirementMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.DetailJobOfferRequirementMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.IndexJobOfferRequirementMvc;
using WebApp.ViewModels.JobOfferRequirementMvc.UpdateJobOfferRequirementMvc;
using WebApp.ViewModels.JobOfferSkillMvc.CreateDetailsJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.DeleteJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.DetailJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.IndexJobOfferSkillMvc;
using WebApp.ViewModels.JobOfferSkillMvc.UpdateJobOfferSkillMvc;

namespace WebApp.Mappings
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