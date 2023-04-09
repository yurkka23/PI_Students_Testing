using Microsoft.AspNetCore.Identity;

namespace EduHub.Domain.Entities;
public class AppUserRole : IdentityUserRole<Guid>
{
    public User User { get; set; }
    public AppRole Role { get; set; }
}