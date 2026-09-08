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

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request, CancellationToken ct = default)
        {
            var result = await _authService.LoginAsync(request, ct);

            Response.Cookies.Append("refreshToken", result.RefreshToken!, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7),
                Path = "/api/auth/refresh"
            });

            return Ok(new { accessToken = result.AccessToken, expiresAt = result.ExpiresAt });
        }
    }
}
