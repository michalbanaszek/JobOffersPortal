using Newtonsoft.Json;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Common.Models
{
    public class ErrorValidationResponse
    {
        public int Status { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
