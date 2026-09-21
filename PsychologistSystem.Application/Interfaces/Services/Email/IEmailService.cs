using PsychologistSystem.Application.Contracts.Email;
using System.Net.Mail;

namespace PsychologistSystem.Application.Interfaces.Services.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailMessage message, CancellationToken ct = default);
    }
}
