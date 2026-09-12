using Microsoft.EntityFrameworkCore;
using PsychologistSystem.Application.Exceptions;
using PsychologistSystem.Application.Interfaces.Repositories.Psychologists;
using PsychologistSystem.Domain.Entity;
using PsychologistSystem.Persistance.Context;

namespace PsychologistSystem.Infrastructure.Repositories.Psychologists
{
    public class PsychologistRepository : IPsychologistRepository
    {
        private readonly ApplicationDbContext _context;

        public PsychologistRepository(ApplicationDbContext context)
        {
             _context = context;
        }
        public void Add(Psychologist psychologist)
        {
            _context.Psychologists.Add(psychologist);
        }

        // getting by psychologist id
        public async Task<Psychologist?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var user = await _context.Psychologists.FirstOrDefaultAsync(x => x.Id == id, ct);

            if (user == null)
            {
                throw new UserNotFoundException("User was not found!");
            }

            return user;
        }
        // getting by user id
        public async Task<Psychologist?> GetByUserIdAsync(Guid userId, CancellationToken ct)
        {
            var user = await _context.Psychologists.FirstOrDefaultAsync(x => x.UserId == userId, ct);

            if (user == null)
            {
                throw new UserNotFoundException("User was not found!");
            }

            return user;
        }
    }
}
