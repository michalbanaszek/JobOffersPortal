using Application.JobOffers.Commands.UpdateJobOffer;
using Swashbuckle.AspNetCore.Filters;

namespace WebUI.SwaggerExamples.Requests
{
    public class UpdateJobOfferCommandExample : IExamplesProvider<UpdateJobOfferCommand>
    {
        public UpdateJobOfferCommand GetExamples()
        {
            return new UpdateJobOfferCommand()
            {            
                Id = "string",
                Position = "Position test",              
                Salary = 2000
            };
        }
    }
}
