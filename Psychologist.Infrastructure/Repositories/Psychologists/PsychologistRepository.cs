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
    }
}
