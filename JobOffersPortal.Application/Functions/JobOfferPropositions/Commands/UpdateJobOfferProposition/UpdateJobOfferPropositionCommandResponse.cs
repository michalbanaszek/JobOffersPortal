using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.UpdateJobOfferProposition
{
    public class UpdateJobOfferPropositionCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public UpdateJobOfferPropositionCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public UpdateJobOfferPropositionCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
