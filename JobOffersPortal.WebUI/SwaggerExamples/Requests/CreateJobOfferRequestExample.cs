using JobOffersPortal.WebUI.Contracts.Requests;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class CreateJobOfferRequestExample : IExamplesProvider<CreateJobOfferRequest>
    {
        public CreateJobOfferRequest GetExamples()
        {
            return new CreateJobOfferRequest()
            {
                Date = DateTime.Now,
                Offers = "Test",
                Skills = "Test",
                Position = "Test",
                Requirements = "Test",
                Salary = 1000
            };
        }
    }
}
