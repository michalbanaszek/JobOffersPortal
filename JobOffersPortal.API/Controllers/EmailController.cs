using App.Metrics;
using JobOffersPortal.API.Metrics;
using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Persistance.EF.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IMetrics _metrics;

        public EmailController(IEmailService emailService, IMetrics metrics)
        {
            _emailService = emailService;
            _metrics = metrics;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] SendEmailRequest request)
        {
            await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.Content, request.Files);

            _metrics.Measure.Counter.Increment(MetricsSendEmail.SentEmailCounter);

            return Ok();
        }
    }
}
