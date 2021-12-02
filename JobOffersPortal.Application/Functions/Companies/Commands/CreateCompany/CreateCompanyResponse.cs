using JobOffersPortal.Application.Common.Models.Responses;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyResponse : Result
    {
        public string Id { get; set; }

        internal CreateCompanyResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }
        public CreateCompanyResponse(string id) : base()
        {
            Id = id;
        }
    }
}
