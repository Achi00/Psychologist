using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Application.Interfaces.Repositories.Psychologists;
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

        public Task<PsychologistApplication?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return _context.PsychologistApplications.FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public Task<PsychologistApplication?> GetPendingByUserIdAsync(Guid userId, CancellationToken ct)
        {
            return _context.PsychologistApplications
                .FirstOrDefaultAsync(a => a.UserId == userId && a.Status == PsychologistApplicationStatus.Pending, ct);
        }

        public Task<IReadOnlyList<PsychologistApplication>> GetPendingAsync(CancellationToken ct)
        {
            return _context.PsychologistApplications
                .Where(a => a.Status == PsychologistApplicationStatus.Pending)
                .OrderBy(a => a.SubmittedAt)
                .ToListAsync(ct)
                .ContinueWith(t => (IReadOnlyList<PsychologistApplication>)t.Result);
        }
    }
}
