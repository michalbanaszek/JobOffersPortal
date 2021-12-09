using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.CreateJobOfferRequirement
{
    public class CreateJobOfferRequirementResponse : BaseResponse
    {
        public string Id { get; set; }

        public CreateJobOfferRequirementResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateJobOfferRequirementResponse(string id) : base()
        {
            Id = id;
        }
    }
}
