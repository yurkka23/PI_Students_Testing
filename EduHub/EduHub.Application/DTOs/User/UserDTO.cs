using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Test;
using EduHub.Application.DTOs.TestResult;

namespace EduHub.Application.DTOs.User;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? AboutMe { get; set; }
    public string? UserImgUrl { get; set; } = string.Empty;
    public DateTimeOffset RegisterTime { get; set; }
 
    public ICollection<CourseDTO>? Courses { get; set; }
    public ICollection<TestDTO>? Tests { get; set; }
    public ICollection<TestResultDTO>? TestResults { get; set; }
}
