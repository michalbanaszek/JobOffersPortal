using System.Collections.Generic;

namespace JobOffersPortal.Application.Common.Models.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
