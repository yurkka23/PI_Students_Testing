using EduHub.Application.DTOs.Course;

namespace EduHub.UI.ViewModels.Teacher;

public class CourseVM
{
    public CourseDTO? Course { get; set; } 
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
