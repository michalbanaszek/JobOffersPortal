using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.DeleteJobOffer
{
    public class DeleteJobOfferCommandResponse : BaseResponse
    {
        public string Id { get; set; }      

        public DeleteJobOfferCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public DeleteJobOfferCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
