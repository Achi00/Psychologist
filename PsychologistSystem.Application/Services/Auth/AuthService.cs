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

        public AuthService(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator, IEmailService emailService)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
        }
        public async Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request)
        {
            var (status, userId, errors) = await _identityService.CreateUserAsync(request.Email, request.UserName, request.Password);

            if (status == IdentityResultStatus.Failed)
            {
                // TODO: add custome exception
                throw new Exception();
            }

            var token = await _identityService.GenerateEmailConfirmationTokenAsync(userId);

            var emailMessage = new EmailMessage(
                To: request.Email,
                Subject: request.UserName,
                PlainTextBody: token,
                HtmlBody: string.Empty
            );
            await _emailService.SendEmailAsync(emailMessage);

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
