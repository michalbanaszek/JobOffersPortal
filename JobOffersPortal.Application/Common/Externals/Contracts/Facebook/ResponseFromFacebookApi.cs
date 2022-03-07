using System.Collections.Generic;

namespace JobOffersPortal.Application.Common.Externals.Contracts.Facebook
{
    public class ResponseFromFacebookApi<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
