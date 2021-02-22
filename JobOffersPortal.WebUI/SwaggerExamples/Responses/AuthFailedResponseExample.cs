using JobOffersPortal.WebUI.Contracts.Responses;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace JobOffersPortal.WebUI.SwaggerExamples.Responses
{
    public class AuthFailedResponseExample : IExamplesProvider<AuthFailedResponse>
    {
        public AuthFailedResponse GetExamples()
        {
            return new AuthFailedResponse()
            {
                Errors = new List<string>()
                {
                    "Validation error"
                }
            };
        }
    }
}
