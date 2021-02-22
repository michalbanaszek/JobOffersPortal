using JobOffersPortal.WebUI.Contracts.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class RegisterRequestExample : IExamplesProvider<RegisterRequest>
    {
        public RegisterRequest GetExamples()
        {
            return new RegisterRequest()
            {
                 Email = "user@gmail.com",
                 Password = "Qwerty!1"
            };
        }
    }
}
