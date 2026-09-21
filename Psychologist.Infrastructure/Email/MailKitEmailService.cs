using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PsychologistSystem.Application.Contracts.Email;
using PsychologistSystem.Application.Interfaces.Services.Email;
using System.Runtime.CompilerServices;

namespace PsychologistSystem.Infrastructure.Email
{
    public sealed class MailKitEmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public MailKitEmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(EmailMessage message, CancellationToken ct = default)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_settings.FromAddress));
            email.To.Add(MailboxAddress.Parse(message.To));
            email.Subject = message.Subject;
            email.Body = new BodyBuilder
            {
                HtmlBody = message.HtmlBody,
                TextBody = message.PlainTextBody
            }.ToMessageBody();

            using var client = new SmtpClient();

            await client.ConnectAsync(_settings.Host, _settings.Port, _settings.UseTls);

            if (!string.IsNullOrEmpty(_settings.Username))
            {
                await client.AuthenticateAsync(_settings.Username, _settings.Password, ct);
            }

            await client.SendAsync(email, ct);
            await client.DisconnectAsync(true, ct);
        }
    }
}
