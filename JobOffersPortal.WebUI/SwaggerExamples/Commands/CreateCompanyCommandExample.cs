using Application.Companies.Commands.CreateCompany;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class CreateCompanyCommandExample : IExamplesProvider<CreateCompanyCommand>
    {
        public CreateCompanyCommand GetExamples()
        {
            return new CreateCompanyCommand()
            {
                Name = "NewCompany"
            };
        }
    }
}
