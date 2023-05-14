using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Test;
using EduHub.Application.DTOs.TestResult;

namespace EduHub.UI.ViewModels.Test;

public class TestVM
{
    public TestDTO? Test { get; set; }
    public IEnumerable<CourseDTO>? Courses { get; set; }
    public IEnumerable<TestResultDTO>? Results { get; set; }

}
