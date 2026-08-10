using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PsychologistSystem.Application.Contracts;
using PsychologistSystem.Application.Contracts.Email;
using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.Exceptions;
using PsychologistSystem.Application.Interfaces;
using PsychologistSystem.Application.Interfaces.JWT;
using PsychologistSystem.Application.Interfaces.Repositories;
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
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            IIdentityService identityService, 
            IJwtTokenGenerator jwtTokenGenerator, 
            IEmailService emailService, 
            IOptions<ClientOptions> options,
            IRefreshTokenService refreshTokenService,
            IRefreshTokenRepository refreshTokenRepository,
            IUnitOfWork unitOfWork,
            ILogger<AuthService> logger)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _emailService = emailService;
            _options = options.Value;
            _refreshTokenService = refreshTokenService;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request)
        {
            var (status, userId, errors) = await _identityService.CreateUserAsync(request.Email, request.Firstname, request.Lastname, request.Password);

            if (status == IdentityResultStatus.Failed)
            {
                throw new UnauthorizedException();
            }

            var token = await _identityService.GenerateEmailConfirmationTokenAsync(userId);

            var confirmationLink = $"{_options.BaseUrl}/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";

            // TODO: seperate email templates IEmailTemplateService or in html files
            await _emailService.SendEmailAsync(new EmailMessage(
                To: request.Email,
                Subject: "Confirm your email",
                HtmlBody: $"<p>Please confirm your account by clicking <a href=\"{confirmationLink}\">here</a>.</p>"
            ));

            return new EmailConfirmationResult(userId, token, request.Email);
        }

        public async Task ConfirmEmailAsync(string userId, string token)
        {
            var confirmed = await _identityService.ConfirmEmailAsync(Guid.Parse(userId), token);

            if (!confirmed)
            {
                throw new ValidationException("Invalid or expired confirmation token.");
            }
        }

        public async Task<AuthResult> LoginAsync(LoginRequest request, CancellationToken ct = default)
        {
            var userId = await _identityService.GetUserIdByEmailAsync(request.Email);

            if (userId == null)
            {
                throw new UnauthorizedException("Invalid credentials");
            }

            var validPassword = await _identityService.CheckPasswordAsync(userId.Value, request.Password);

            if (!validPassword)
            {
                throw new UnauthorizedException("Invalid credentials");
            }

            var roles = await _identityService.GetRolesAsync(userId.Value);
            var accessToken = _jwtTokenGenerator.GenerateToken(userId.Value, request.Email, roles);

            // refresh token generation hashing
            var rawRefreshToken = _refreshTokenService.GenerateToken();
            var tokenHash = _refreshTokenService.HashToken(rawRefreshToken);

            _refreshTokenRepository.Add(new Domain.Entity.RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId.Value,
                TokenHash = tokenHash,
                ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
                CreatedAt = DateTimeOffset.UtcNow
            });

            await _unitOfWork.SaveChangesAsync(ct);

            return new AuthResult(accessToken, DateTime.UtcNow.AddMinutes(60));
        }

        public async Task LogoutAsync(Guid userId)
        {

        }

        public async Task ForgotPasswordAsync(string email)
        {
            var userId = await _identityService.GetUserIdByEmailAsync(email);

            if (userId == null)
            {
                return;
            }

            var token = await _identityService.GeneratePasswordResetTokenAsync(userId.Value);
            var resetLink = $"{_options.BaseUrl}/reset-password?userId={userId}&token={Uri.EscapeDataString(token)}";

            // TODO: return html file in future instead
            await _emailService.SendEmailAsync(new EmailMessage(
                To: email,
                Subject: "Reset your password",
                HtmlBody: $"<p>Reset your password <a href=\"{resetLink}\">here</a>. This link expires shortly.</p>"
            ));
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var success = await _identityService.ResetPasswordAsync(request.UserId, request.Token, request.NewPassword);

            if (!success)
            {
                throw new InvalidOperationException("Invalid or expired reset token, or new password does not meet requirements.");
            }
        }

        public Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
