using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JobOffersPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] SendEmailRequest emailRequest)
        {
            await _emailService.SendEmailAsync(emailRequest);

            return Ok();
        }
    }
}
