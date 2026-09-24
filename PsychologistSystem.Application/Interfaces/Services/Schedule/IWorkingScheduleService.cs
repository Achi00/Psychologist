using PsychologistSystem.Application.DTOs.Schedule;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Services.Schedule
{
    public interface IWorkingScheduleService
    {
        public Task<Guid> CreateAsync(CreateWorkingScheduleRequest request, CancellationToken ct);
        public Task<List<WorkingScheduleResponse>> GetMineAsync(CancellationToken ct);
        public Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
