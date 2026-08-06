namespace PsychologistSystem.Application.DTOs.Auth
{
    // uses IHttpContextAccessor, reads from ClaimsPrincipal on HttpContext, in API layer
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Email { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
