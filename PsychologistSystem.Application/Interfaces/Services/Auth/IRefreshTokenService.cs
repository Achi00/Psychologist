namespace PsychologistSystem.Application.Interfaces.Services.Auth
{
    public interface IRefreshTokenService
    {
        string GenerateToken();
        string HashToken(string token);
    }
}
