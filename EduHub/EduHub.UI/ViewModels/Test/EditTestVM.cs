using EduHub.Application.DTOs.Course;
using EduHub.Application.Models.Test;

namespace EduHub.UI.ViewModels.Test;

public class EditTestVM
{
    public EditTestModel EditTest { get; set; } = new EditTestModel();
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
