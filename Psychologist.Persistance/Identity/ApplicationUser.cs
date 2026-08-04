using Microsoft.AspNetCore.Identity;
using Psychologist.Domain.Enums;

namespace Psychologist.Persistance.Identity
{
    public sealed class ApplicationUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateOnly BirthDate { get; set; }

        public Gender Gender { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public Role Role { get; set; }
    }
}
