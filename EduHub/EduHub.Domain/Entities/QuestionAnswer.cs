namespace EduHub.Domain.Entities;

public class QuestionAnswer : BaseEntity
{
    public Guid QuestionId { get; set; }
    public string Answer { get; set; } = default!;
    public Guid PassingTestId { get; set; }
    public PassingTest PassingTest { get; set; }
}
