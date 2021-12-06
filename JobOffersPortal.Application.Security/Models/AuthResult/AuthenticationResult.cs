using System.Collections.Generic;

namespace JobOffersPortal.Application.Security.Models.AuthResult
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
