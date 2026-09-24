using PsychologistSystem.Application.DTOs.Booking;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Services.Schedile
{
    public interface IWorkingScheduleService
    {
        public Task<bool> CreateAsync(CreateWorkingScheduleRequest request, CancellationToken ct);
        public Task<WorkingSchedule?> GetMineAsync(CancellationToken ct);
        public Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    }
}
