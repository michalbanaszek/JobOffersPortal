using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteJobOfferSkillCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public DeleteJobOfferSkillCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public DeleteJobOfferSkillCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
