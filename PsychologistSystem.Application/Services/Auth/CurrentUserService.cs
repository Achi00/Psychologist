using Microsoft.AspNetCore.Http;
using PsychologistSystem.Application.Interfaces.Services.Auth;
using System.Security.Claims;

namespace PsychologistSystem.Application.Services.Auth
{
    public sealed class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(IHttpContextAccessor accessor) => _accessor = accessor;

        private ClaimsPrincipal? User => _accessor.HttpContext?.User;

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;

        public Guid? UserId
        {
            get
            {
                var value = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return Guid.TryParse(value, out var id) ? id : null;
            }
        }
        public string? Email => User?.FindFirst(ClaimTypes.Email)?.Value;

        public bool IsInRole(string role) => User?.IsInRole(role) ?? false;
    }
}
