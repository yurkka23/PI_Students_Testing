using EduHub.Application.DTOs.Course;
using EduHub.Application.Models.Course;

namespace EduHub.UI.ViewModels.Teacher;

public class EditCourseVM
{
    public EditCourseModel EditCourse { get; set; } = new EditCourseModel();
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
