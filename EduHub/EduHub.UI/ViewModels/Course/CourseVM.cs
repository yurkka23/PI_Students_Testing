using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.TestResult;

namespace EduHub.UI.ViewModels.Teacher;

public class CourseVM
{
    public CourseDTO? Course { get; set; } 
    public IEnumerable<CourseDTO>? Courses { get; set; }
    public IEnumerable<TestResultDTO>? TestResults { get; set; }
}
