namespace JobOffersPortal.Infrastructure.Security.Contracts.Identity.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
