using Application.Common.Interfaces;
using Application.Emails.Commands;
using Infrastructure.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        public EmailService(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }

        public async Task SendEmailAsync(SendEmailCommand command)
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
