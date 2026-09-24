using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Application.Interfaces.Repositories.Schedule;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.Infrastructure.Repositories.Schedule
{
    public class WorkingScheduleRepository : IWorkingScheduleRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkingScheduleRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(WorkingSchedule schedule)
        {
            _context.WorkingSchedules.Add(schedule);
        }

        // should use tracking used in delete
        public async Task<WorkingSchedule?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.WorkingSchedules.FirstOrDefaultAsync(s => s.Id == id, ct);
        }

        public async Task<List<WorkingSchedule>> GetByPsychologistAndDayAsync(Guid psychologistId, DayOfWeek day, CancellationToken ct = default)
        {
            return await _context.WorkingSchedules.AsNoTracking().Where(s => s.PsychologistId == psychologistId && s.DayOfWeek == day).ToListAsync(ct);
        }

        public async Task<List<WorkingSchedule>> GetByPsychologistIdAsync(Guid psychologistId, CancellationToken ct = default)
        {
            return await _context.WorkingSchedules.AsNoTracking().Where(s => s.PsychologistId == psychologistId).ToListAsync(ct);
        }

        public void Remove(WorkingSchedule schedule)
        {
            _context.WorkingSchedules.Remove(schedule);
        }
    }
}
