using System.Collections.Generic;

namespace WebApp.ClientServices.Responses
{
    public class ResponseFromApi<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
