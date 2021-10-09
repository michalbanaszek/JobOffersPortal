using Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Emails.Commands
{
    public class SendEmailCommand : IRequest<Unit>
    {
        public string ToEmail { get; set; }        
        public string Subject { get; set; }
		public string Content { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    public class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly ILogger<SendEmailCommandHandler> _logger;
        private IEmailService _emailService;

        public SendEmailCommandHandler(ILogger<SendEmailCommandHandler> logger, IEmailService emailService)
        {
            _logger = logger;

            _emailService = emailService;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailAsync(request);

            return Unit.Value;
        }
    }
}
