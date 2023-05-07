using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Test;
using EduHub.Application.Models.Question;

namespace EduHub.UI.ViewModels.Question;

public class AddQuestionVM
{
    public AddQuestionModel AddQuestion { get; set; } = new AddQuestionModel();
    public TestDTO? Test { get; set; }
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
