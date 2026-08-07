namespace PsychologistSystem.Application.Interfaces.JWT
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(Guid userId, string email, IEnumerable<string> roles);
    }
}
