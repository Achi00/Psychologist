namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record SubmitPsychologistApplicationRequest(
        string Description,
        Guid CategoryId
    );
}
