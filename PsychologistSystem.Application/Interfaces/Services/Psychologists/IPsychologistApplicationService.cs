using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Interfaces.Services.Psychologists
{
    public interface IPsychologistApplicationService
    {
        Task SubmitApplicationAsync(Guid userId, SubmitPsychologistApplicationRequest request, CancellationToken ct);
        Task ApproveApplicationAsync(Guid applicationId, Guid adminUserId, CancellationToken ct);
        Task RejectApplicationAsync(Guid applicationId, Guid adminUserId, string reason, CancellationToken ct);
    }
}
