using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Common.Models
{
    public abstract class BaseResponse
    {
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        protected BaseResponse()
        {
            Succeeded = true;
            Errors = Array.Empty<string>();
        }

        protected BaseResponse(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();          
        }
    }
}
