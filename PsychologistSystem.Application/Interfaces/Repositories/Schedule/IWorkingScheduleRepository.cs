using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories.Schedule
{
    public interface IWorkingScheduleRepository
    {
        void Add(WorkingSchedule schedule);
        void Remove(WorkingSchedule schedule);
        Task<WorkingSchedule?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<List<WorkingSchedule>> GetByPsychologistIdAsync(Guid psychologistId, CancellationToken ct = default);
        Task<List<WorkingSchedule>> GetByPsychologistAndDayAsync(Guid psychologistId, DayOfWeek day, CancellationToken ct = default);
    }
}
