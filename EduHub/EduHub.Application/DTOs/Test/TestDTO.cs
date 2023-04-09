using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Question;
using EduHub.Application.DTOs.TestResult;
using EduHub.Application.DTOs.User;

namespace EduHub.Application.DTOs.Test;

public class TestDTO : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int DurationMinutes { get; set; }

    public UserDTO? Teacher { get; set; }
    public ICollection<TestResultDTO>? TestResults { get; set; }
    public ICollection<QuestionDTO>? Questions { get; set; }
}
