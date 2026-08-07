using MailKit.Net.Smtp;
using PsychologistSystem.Application.Contracts.Email;
using PsychologistSystem.Application.Interfaces.Services.Email;

namespace PsychologistSystem.Infrastructure.Email
{
    public sealed class SmtpEmailservice : IEmailService
    {
        public Task SendEmailAsync(EmailMessage message, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
