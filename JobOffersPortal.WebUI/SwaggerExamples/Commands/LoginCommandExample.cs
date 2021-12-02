using JobOffersPortal.Application.Common.Models.Requests;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.API.SwaggerExamples.Commands
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
