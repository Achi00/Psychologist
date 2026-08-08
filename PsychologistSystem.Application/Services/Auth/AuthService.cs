using Microsoft.Extensions.Options;
using PsychologistSystem.Application.Contracts;
using PsychologistSystem.Application.Contracts.Email;
using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.Interfaces.JWT;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Application.Interfaces.Services.Email;
using PsychologistSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PsychologistSystem.Application.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IEmailService _emailService;
        private readonly ClientOptions _options;
        //private readonly I

        public AuthService(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator, IEmailService emailService, IOptions<ClientOptions> options)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
            _options = options.Value;
        }
        public async Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request)
        {
            var (status, userId, errors) = await _identityService.CreateUserAsync(request.Email, request.Firstname, request.Lastname, request.Password);

            if (status == IdentityResultStatus.Failed)
            {
                // TODO: add custome exception
                throw new Exception();
            }

            var token = await _identityService.GenerateEmailConfirmationTokenAsync(userId);

            var confirmationLink = $"{_options.BaseUrl}/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";

            // TODO: seperate email templates IEmailTemplateService or in html files
            await _emailService.SendEmailAsync(new EmailMessage(
                To: request.Email,
                Subject: "Confirm your email",
                HtmlBody: $"<p>Please confirm your account by clicking <a href=\"{confirmationLink}\">here</a>.</p>"
            ));

            return new EmailConfirmationResult
            {
                UserId = userId,
                Token = token,
                Email = request.Email
            };
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var confirmed = await _identityService.ConfirmEmailAsync(Guid.Parse(userId), token);

            if (!confirmed)
            {
                throw new ValidationException("Invalid or expired confirmation token.");
            }
        }
    }
}
