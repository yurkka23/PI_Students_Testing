using Microsoft.AspNetCore.Identity;

namespace EduHub.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? AboutMe { get; set; }
        public string? UserImgUrl { get; set; } = string.Empty;
        public DateTimeOffset RegisterTime { get; set; }


        public Guid? VerificationCode { get; set; }
        public DateTimeOffset? VerificationExpires { get; set; }
        //relations
        public ICollection<AppUserRole> UserRoles { get; set; }
        public ICollection<Course> Courses { get; set; }
        public ICollection<Test> Tests { get; set; }
        public ICollection<UserCourse> StudentsCourses { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}