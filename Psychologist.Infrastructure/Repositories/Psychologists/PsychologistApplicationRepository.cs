using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Domain.Enums;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.Infrastructure.Repositories.Psychologists
{
    public class PsychologistApplicationRepository : IPsychologistApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public PsychologistApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(PsychologistApplication application)
        {
            _context.PsychologistApplications.Add(application);
        }

        public Task<PsychologistApplication?> GetByIdAsync(Guid id)
        {
            return _context.PsychologistApplications.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task<PsychologistApplication?> GetPendingByUserIdAsync(Guid userId)
        {
            return _context.PsychologistApplications
                .FirstOrDefaultAsync(a => a.UserId == userId && a.Status == PsychologistApplicationStatus.Pending);
        }

        public Task<IReadOnlyList<PsychologistApplication>> GetPendingAsync()
        {
            return _context.PsychologistApplications
                .Where(a => a.Status == PsychologistApplicationStatus.Pending)
                .OrderBy(a => a.SubmittedAt)
                .ToListAsync()
                .ContinueWith(t => (IReadOnlyList<PsychologistApplication>)t.Result);
        }
    }
}
