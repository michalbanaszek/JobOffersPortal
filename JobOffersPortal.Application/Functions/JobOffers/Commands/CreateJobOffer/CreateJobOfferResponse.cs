using JobOffersPortal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOffers.Commands.CreateJobOffer
{
    public class CreateJobOfferResponse : BaseResponse
    {
        public string Id { get; set; }
        public Uri Uri { get; set; }

        public CreateJobOfferResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateJobOfferResponse(string id, Uri uri) : this(id)
        {
            Uri = uri;
        }

        public CreateJobOfferResponse(string id) : base()
        {
            Id = id;
        }
    }
}
