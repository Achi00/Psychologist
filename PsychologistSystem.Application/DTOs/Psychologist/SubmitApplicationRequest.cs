namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record SubmitApplicationRequest(
        string Description,
        Guid CategoryId
    );
}
