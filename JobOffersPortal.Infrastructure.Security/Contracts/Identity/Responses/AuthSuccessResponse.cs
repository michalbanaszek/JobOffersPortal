namespace JobOffersPortal.Infrastructure.Security.Contracts.Identity.Responses
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
