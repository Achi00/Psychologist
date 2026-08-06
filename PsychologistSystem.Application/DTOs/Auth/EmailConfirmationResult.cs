namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed class EmailConfirmationResult
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
