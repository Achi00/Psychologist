using PsychologistSystem.Application.DTOs.Booking;
using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Services.Schedile
{
    public interface IWorkingScheduleService
    {
        public Task<Guid> CreateAsync(CreateWorkingScheduleRequest request, CancellationToken ct);
        public Task<List<WorkingSchedule?>> GetMineAsync(CancellationToken ct);
        public Task DeleteAsync(Guid id, CancellationToken ct);
    }
}
