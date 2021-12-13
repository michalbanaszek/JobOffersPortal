using JobOffersPortal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferCommandResponse : BaseResponse
    {
        public string Id { get; set; }
        public Uri Uri { get; set; }

        public CreateJobOfferCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateJobOfferCommandResponse(string id, Uri uri) : this(id)
        {
            Uri = uri;
        }

        public CreateJobOfferCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
