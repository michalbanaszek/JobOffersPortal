using JobOffersPortal.Infrastructure.Security.Contracts.Identity.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.API.SwaggerExamples
{
    public class LoginCommandExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest()
            {
                Email = "user1@gmail.com",
                Password = "Qwerty!1"
            };
        }
    }
}
