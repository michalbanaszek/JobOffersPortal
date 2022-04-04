using System;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillCommandResponse
    { 
        public Uri Uri { get; set; }

        public CreateJobOfferSkillCommandResponse(Uri uri)
        {
            Uri = uri;
        }
    }
}
