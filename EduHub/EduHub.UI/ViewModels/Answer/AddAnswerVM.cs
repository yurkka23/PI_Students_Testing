using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Question;
using EduHub.Application.DTOs.Test;
using EduHub.Application.Models.Answer;

namespace EduHub.UI.ViewModels.Answer;

public class AddAnswerVM
{
    public AddAnswerModel AddAnswer { get; set; } = new AddAnswerModel();
    public TestDTO? Test { get; set; }
    public QuestionDTO? Queastion { get; set; }
    public IEnumerable<CourseDTO>? Courses { get; set; }
}
