

namespace EduHub.Domain.Entities;

public class Test : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int DurationMinutes { get; set; }

    //relations
    public Guid TeacherId { get; set; }
    public User Teacher { get; set; } 
    public Guid CourseId {  get; set; }
    public Course Course { get; set; }
    public ICollection<TestResult> TestResults { get; set; }
    public ICollection<Question> Questions { get; set; }

}
