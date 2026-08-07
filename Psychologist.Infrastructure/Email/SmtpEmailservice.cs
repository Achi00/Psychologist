using MailKit.Net.Smtp;
using PsychologistSystem.Application.Interfaces.Services.Email;

namespace PsychologistSystem.Infrastructure.Email
{
    public sealed class SmtpEmailservice : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string htmlBody, CancellationToken ct = default)
        {
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("localhost", 1025);
        }
    }
}
