namespace EduHub.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public string? Password { get; set; } //if student wants to add to course

        //relations
        public Guid TeacherId { get; set; }
        public User Teacher { get; set; }

        public ICollection<UserCourse> StudentsCourses { get; set; }
        public ICollection<Test> Tests { get; set; }
    }
}