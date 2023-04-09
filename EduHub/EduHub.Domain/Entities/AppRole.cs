using Microsoft.AspNetCore.Identity;

namespace EduHub.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
}