namespace JobOffersPortal.Application.Security.Models.AuthResult
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
