using Application.Emails.Commands;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        public Task SendEmailAsync(SendEmailCommand command);
    }
}
