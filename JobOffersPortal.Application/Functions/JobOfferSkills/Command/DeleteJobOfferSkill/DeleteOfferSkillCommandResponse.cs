using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferSkills.Command.DeleteJobOfferSkill
{
    public class DeleteOfferSkillCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public DeleteOfferSkillCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public DeleteOfferSkillCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
