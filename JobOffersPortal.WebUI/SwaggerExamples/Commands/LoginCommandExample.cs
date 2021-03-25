using Application.Identity.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class LoginCommandExample : IExamplesProvider<LoginCommand>
    {
        public LoginCommand GetExamples()
        {
            return new LoginCommand()
            {
                Email = "user1@gmail.com",
                Password = "Qwerty!1"
            };
        }
    }
}
