using PsychologistSystem.Application.DTOs.Auth;

namespace PsychologistSystem.Application.Interfaces.Services.Psychologists
{
    public interface IPsychologistApplicationService
    {
        Task SubmitApplicationAsync(Guid userId, SubmitPsychologistApplicationRequest request);
        Task ApproveApplicationAsync(Guid applicationId, Guid adminUserId);
        Task RejectApplicationAsync(Guid applicationId, Guid adminUserId, string reason);
    }
}
