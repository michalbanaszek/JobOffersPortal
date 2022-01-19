using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public CreateJobOfferPropositionCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateJobOfferPropositionCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
