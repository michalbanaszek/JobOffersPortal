using JobOffersPortal.Application.Common.Interfaces;
using JobOffersPortal.Application.Common.Models.Requests;
using JobOffersPortal.Persistance.EF.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace JobOffersPortal.Persistance.EF.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendEmailAsync(SendEmailRequest command)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(_emailOptions.SmtpUsername);

            email.To.Add(MailboxAddress.Parse(command.ToEmail));

            email.Subject = command.Subject;

            var builder = new BodyBuilder();

            if (command.Files != null)
            {
                byte[] fileBytes;

                foreach (var file in command.Files)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }

                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }

            builder.HtmlBody = command.Content;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailOptions.SmtpServer, _emailOptions.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailOptions.SmtpUsername, _emailOptions.SmtpPassword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }
    }
}
