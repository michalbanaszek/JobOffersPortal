using JobOffersPortal.WebUI.Contracts.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class LoginRequestExample : IExamplesProvider<LoginRequest>
    {
        public LoginRequest GetExamples()
        {
            return new LoginRequest()
            {
                Email = "user@gmail.com",
                Password = "Qwerty!1"
            };
        }
    }
}
