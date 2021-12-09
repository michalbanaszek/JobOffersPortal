using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.JobOfferPropositions.Commands.CreateJobOfferProposition
{
    public class CreateJobOfferPropositionResponse : BaseResponse
    {
        public string Id { get; set; }

        public CreateJobOfferPropositionResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateJobOfferPropositionResponse(string id) : base()
        {
            Id = id;
        }
    }
}
