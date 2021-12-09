using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.UpdateJobOfferSkill
{
    public class UpdateJobOfferSkillCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public UpdateJobOfferSkillCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public UpdateJobOfferSkillCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}