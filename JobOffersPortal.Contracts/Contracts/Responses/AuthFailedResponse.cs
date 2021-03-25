using System.Collections.Generic;

namespace JobOffersPortal.WebUI.Contracts.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
