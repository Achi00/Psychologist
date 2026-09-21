using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.DTOs.Psychologist;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Services.Psychologists
{
    public interface IPsychologistApplicationService
    {
        Task SubmitApplicationAsync(Guid userId, SubmitApplicationRequest request, CancellationToken ct);
        Task ApproveApplicationAsync(Guid applicationId, Guid adminUserId, CancellationToken ct);
        Task RejectApplicationAsync(Guid applicationId, Guid adminUserId, RejectApplicationRequest request, CancellationToken ct);
        Task<IReadOnlyList<PsychologistApplication>> GetPendingApplicationsAsync(CancellationToken ct);
    }
}
