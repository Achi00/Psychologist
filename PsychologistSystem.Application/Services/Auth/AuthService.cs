using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.Interfaces.JWT;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using PsychologistSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PsychologistSystem.Application.Services.Auth
{
    public sealed class AuthService : IAuthService
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        //TODO: add email service for notifications

        public AuthService(IIdentityService identityService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _identityService = identityService;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<EmailConfirmationResult> RegisterAsync(RegisterUserRequest request)
        {
            //var (status, userId, errors) = await _identityService.CreateUserAsync(request.Email, request.UserName, request.Password);

            //if (status == IdentityResultStatus.Failed)
            //{
            //    throw new Exception();
            //}

            //var token = await _identityService.GenerateEmailConfirmationTokenAsync(userId);
            throw new NotImplementedException();
            
        }

        public Task ConfirmEmailAsync(string userId, string token)
        {
            throw new NotImplementedException();
        }
    }
}
