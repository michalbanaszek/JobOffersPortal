using JobOffersPortal.Application.Common.Models.Responses;
using MediatR;

namespace JobOffersPortal.Application.Common.Models.Requests
{
    public class RefreshTokenRequest : IRequest<AuthenticationResult>
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
