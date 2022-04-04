using Newtonsoft.Json;

namespace JobOffersPortal.Application.Common.Models
{
    public class ErrorModelResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
