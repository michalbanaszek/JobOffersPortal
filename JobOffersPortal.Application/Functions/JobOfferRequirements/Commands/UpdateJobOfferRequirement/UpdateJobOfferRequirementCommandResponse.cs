using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.UpdateJobOfferRequirement
{
    public  class UpdateJobOfferRequirementCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public UpdateJobOfferRequirementCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public UpdateJobOfferRequirementCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
