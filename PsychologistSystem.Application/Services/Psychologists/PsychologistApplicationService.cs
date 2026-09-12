using PsychologistSystem.Application.DTOs.Auth;
using PsychologistSystem.Application.Interfaces.Services.Psychologists;

namespace PsychologistSystem.Application.Services.Psychologists
{
    public class PsychologistApplicationService : IPsychologistApplicationService
    {
        private readonly 
        public Task ApproveApplicationAsync(Guid applicationId, Guid adminUserId)
        {
            throw new NotImplementedException();
        }

        public Task RejectApplicationAsync(Guid applicationId, Guid adminUserId, string reason)
        {
            throw new NotImplementedException();
        }

        public Task SubmitApplicationAsync(Guid userId, SubmitPsychologistApplicationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
