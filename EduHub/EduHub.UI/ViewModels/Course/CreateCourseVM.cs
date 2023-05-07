using EduHub.Application.DTOs.Course;
using EduHub.Application.Models.Course;
using System.ComponentModel.DataAnnotations;

namespace EduHub.UI.ViewModels;
public class CreateCourseVM
{
    public CreateCourseModel CreateCourse { get; set; } = new CreateCourseModel();
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
