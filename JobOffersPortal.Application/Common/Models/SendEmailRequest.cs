using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace JobOffersPortal.Application.Common.Models
{
    public class SendEmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
