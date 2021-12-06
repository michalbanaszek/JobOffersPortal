using MediatR;

namespace JobOffersPortal.Application.Security.Models.AuthResult
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
