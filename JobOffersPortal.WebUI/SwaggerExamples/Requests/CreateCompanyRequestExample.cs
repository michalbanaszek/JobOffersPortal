using JobOffersPortal.WebUI.Contracts.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class CreateCompanyRequestExample : IExamplesProvider<CreateCompanyRequest>
    {
        public CreateCompanyRequest GetExamples()
        {
            return new CreateCompanyRequest()
            {
                Name = "NewCompany"
            };
        }
    }
}
