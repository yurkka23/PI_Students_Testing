using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Test;

namespace EduHub.UI.ViewModels.Test;

public class TestVM
{
    public TestDTO? Test { get; set; }
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
