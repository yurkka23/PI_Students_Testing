using EduHub.Application.DTOs.Course;
using EduHub.Application.Models.Test;

namespace EduHub.UI.ViewModels.Test;

public class AddTestVM
{
    public AddTestModel AddTest { get; set; } = new AddTestModel();
    public CourseDTO? Course { get; set; }
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
