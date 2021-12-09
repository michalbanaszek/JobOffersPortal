using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.Companies.Commands.UpdateCompany
{
    public class UpdateCompanyCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public UpdateCompanyCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public UpdateCompanyCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
