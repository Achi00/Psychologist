namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record SubmitPsychologistApplicationRequest(
        string Email,
        string Firstname,
        string Lastname,
        string Description,
        string LicenseNumber
    );
}
