using EduHub.Application.DTOs.Test;
using EduHub.Application.DTOs.User;

namespace EduHub.Application.DTOs.Course;

public class CourseDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Password { get; set; }//if student wants to add to course
    public UserDTO? Teacher { get; set; }
    public ICollection<TestDTO>? Tests { get; set; }
}
