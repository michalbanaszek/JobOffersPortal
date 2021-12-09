using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.CreateJobOfferSkill
{
    public class CreateJobOfferSkillResponse : BaseResponse
    {
        public string Id { get; set; }

        public CreateJobOfferSkillResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateJobOfferSkillResponse(string id) : base()
        {
            Id = id;
        }
    }
}
