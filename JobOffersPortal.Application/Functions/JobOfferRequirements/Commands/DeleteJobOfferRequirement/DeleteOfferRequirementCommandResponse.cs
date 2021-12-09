﻿using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.JobOfferRequirements.Commands.DeleteJobOfferRequirement
{
    public class DeleteOfferRequirementCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public DeleteOfferRequirementCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public DeleteOfferRequirementCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
