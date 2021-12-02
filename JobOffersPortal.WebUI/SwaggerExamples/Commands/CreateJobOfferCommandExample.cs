using Application.JobOffers.Commands.CreateJobOffer;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.API.SwaggerExamples.Commands
{
    public class CreateJobOfferCommandExample : IExamplesProvider<CreateJobOfferCommand>
    {
        public CreateJobOfferCommand GetExamples()
        {
            return new CreateJobOfferCommand()
            {
                CompanyId = "string",
                Position = "Position test",
                Requirements = new string[] { "requirements test 1", "requirements test 2" },
                Skills = new string[] { "skills test 1", "skills test 2" },
                Propositions = new string[] { "propositions test 1", "propositions test 2" },
                Salary = "2000-3000"
            };
        }
    }
}
