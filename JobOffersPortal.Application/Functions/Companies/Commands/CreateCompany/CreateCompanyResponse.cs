using JobOffersPortal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyResponse : Result
    {
        public string Id { get; set; }
        public Uri Uri { get; set; }

        internal CreateCompanyResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public CreateCompanyResponse(string id) : base()
        {
            Id = id;          
        }

        public CreateCompanyResponse(string id, Uri uri) : base()
        {
            Id = id;
            Uri = uri;
        }
    }
}
