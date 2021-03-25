using Application.Identity.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace JobOffersPortal.WebUI.SwaggerExamples.Requests
{
    public class RegisterCommandExample : IExamplesProvider<RegisterCommand>
    {
        public RegisterCommand GetExamples()
        {
            return new RegisterCommand()
            {
                 Email = "user3@gmail.com",
                 Password = "Qwerty!1"
            };
        }
    }
}
