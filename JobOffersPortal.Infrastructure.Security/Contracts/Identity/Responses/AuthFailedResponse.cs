using System.Collections.Generic;

namespace JobOffersPortal.Infrastructure.Security.Contracts.Identity.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
