
namespace EduHub.Domain.Entities;

public class UserCourse
{
    public Guid StudentId { get; set; }
    public User Student { get; set; }
    public Guid CourseId { get; set; }  
    public Course Course { get; set; }
    public DateTimeOffset AddedToCourse { get; set; }
}
