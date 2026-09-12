namespace PsychologistSystem.Application.DTOs.Auth
{
    public sealed record PsychologistApplicationRequest(
        string Email,
        string Firstname,
        string Lastname,
        string Password,
        string Description,
        string LicenseNumber
    );
}
