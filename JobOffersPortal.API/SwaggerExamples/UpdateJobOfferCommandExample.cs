using JobOffersPortal.Application.Functions.JobOffers.Commands.UpdateJobOffer;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.API.SwaggerExamples
{
    public class UpdateJobOfferCommandExample : IExamplesProvider<UpdateJobOfferCommand>
    {
        public UpdateJobOfferCommand GetExamples()
        {
            return new UpdateJobOfferCommand()
            {
                Id = "string",
                Position = "Position test",
                Salary = "2000-3000"
            };
        }
    }
}
