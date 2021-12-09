using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferPropositions.Commands.DeleteJobOfferProposition
{
    public class DeleteJobOfferPropositionCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public DeleteJobOfferPropositionCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public DeleteJobOfferPropositionCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
