using JobOffersPortal.WebUI.Contracts.Responses;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace JobOffersPortal.WebUI.SwaggerExamples.Responses
{
    public class AuthSuccessResponseExample : IExamplesProvider<AuthSuccessResponse>
    {
        public AuthSuccessResponse GetExamples()
        {
            return new AuthSuccessResponse()
            {
                Token = Guid.NewGuid().ToString() + "." + Guid.NewGuid().ToString() + Guid.NewGuid().ToString() + "." + Guid.NewGuid().ToString(),
                RefreshToken = Guid.NewGuid().ToString()
            };
        }
    }
}
