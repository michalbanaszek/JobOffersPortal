using System;

namespace JobOffersPortal.Application.Functions.Companies.Commands.CreateCompany
{
    public class CreateCompanyCommandResponse
    {
        public Uri Uri { get; set; }

        public CreateCompanyCommandResponse(Uri uri)
        {            
            Uri = uri;
        }
    }
}
