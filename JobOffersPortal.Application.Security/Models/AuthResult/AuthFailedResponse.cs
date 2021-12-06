using System.Collections.Generic;

namespace JobOffersPortal.Application.Security.Models.AuthResult
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
