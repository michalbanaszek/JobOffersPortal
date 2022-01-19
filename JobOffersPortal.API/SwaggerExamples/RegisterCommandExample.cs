using JobOffersPortal.Infrastructure.Security.Contracts.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.API.SwaggerExamples
{
    public class RegisterCommandExample : IExamplesProvider<RegisterRequest>
    {
        public RegisterRequest GetExamples()
        {
            return new RegisterRequest()
            {
                Email = "user3@gmail.com",
                Password = "Qwerty!1"
            };
        }
    }
}
