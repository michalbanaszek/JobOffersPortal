using JobOffersPortal.Application.Common.Models;
using System.Collections.Generic;
using System.Linq;

namespace JobOffersPortal.Application.Functions.Companies.Commands.DeleteCompany
{
    public class DeleteCompanyCommandResponse : BaseResponse
    {
        public string Id { get; set; }

        public DeleteCompanyCommandResponse(bool succeeded, IEnumerable<string> errors) : base(succeeded, errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public DeleteCompanyCommandResponse(string id) : base()
        {
            Id = id;
        }
    }
}
