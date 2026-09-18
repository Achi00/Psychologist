using Microsoft.AspNetCore.Mvc;
using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.Interfaces.Services.Auth;

namespace PsychologistSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        private void SetRefreshTokenCookie(string token)
        {
            Response.Cookies.Append("refreshToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/api/auth/refresh"
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(new { userId = result.UserId, email = result.Email });
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailRequest request)
        {
            await _authService.ConfirmEmailAsync(request.UserId, request.Token);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct = default)
        {
            var result = await _authService.LoginAsync(request, ct);

            SetRefreshTokenCookie(result.RefreshToken!);

            return Ok(new { accessToken = result.AccessToken, expiresAt = result.ExpiresAt });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();
            }

            var result = await _authService.RefreshTokenAsync(refreshToken);

            SetRefreshTokenCookie(result.RefreshToken!);

            return Ok(new { accessToken = result.AccessToken, expiresAt = result.ExpiresAt });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _authService.LogoutAsync(refreshToken);
            }

            Response.Cookies.Delete("refreshToken", new CookieOptions { Path = "/api/auth/refresh" });
            return NoContent();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            await _authService.ForgotPasswordAsync(request);
            return Ok(new { message = "If that email is registered, a reset link has been sent." });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _authService.ResetPasswordAsync(request);
            return NoContent();
        }

        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmation(ResendConfirmationRequest request)
        {
            await _authService.ResendConfirmationEmailAsync(request);
            return Ok(new { message = "If that email is registered and unconfirmed, a new confirmation link has been sent." });
        }
    }
}
