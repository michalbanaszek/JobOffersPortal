using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;

namespace Application.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public class UpdateJobOfferSkillViewModel : IMapFrom<JobOfferSkill>
    {
        public string Id { get; set; }
        public string Content { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<JobOfferSkill, UpdateJobOfferSkillViewModel>().ReverseMap();
        }
    }
}
