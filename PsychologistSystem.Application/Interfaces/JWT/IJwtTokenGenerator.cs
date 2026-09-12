namespace PsychologistSystem.Application.Interfaces.JWT
{
    public interface IJwtTokenGenerator
    {
        (string AccessToken, DateTime ExpiresAt) GenerateToken(Guid userId, string email, IEnumerable<string> roles);
    }
}
