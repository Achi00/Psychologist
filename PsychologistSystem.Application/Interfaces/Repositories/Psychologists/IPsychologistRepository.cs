using PsychologistSystem.Domain.Entity;

namespace PsychologistSystem.Application.Interfaces.Repositories.Psychologists
{
    public interface IPsychologistRepository
    {
        void Add(Psychologist psychologist);
    }
}
