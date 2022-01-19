using MediatR;

namespace JobOffersPortal.Infrastructure.Security.Contracts.Identity.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
