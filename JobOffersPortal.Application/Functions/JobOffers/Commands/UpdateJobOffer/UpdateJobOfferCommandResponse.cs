using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer
{
    public class UpdateJobOfferCommandResponse : BaseResponse
    {
        public string Id { get; set; }
       

        public UpdateJobOfferCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public UpdateJobOfferCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
