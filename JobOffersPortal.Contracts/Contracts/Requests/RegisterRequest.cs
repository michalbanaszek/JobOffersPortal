using System.ComponentModel.DataAnnotations;

namespace JobOffersPortal.WebUI.Contracts.Requests
{
    public class RegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
