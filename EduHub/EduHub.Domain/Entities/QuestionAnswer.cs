namespace EduHub.Domain.Entities;

public class QuestionAnswer : BaseEntity
{
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
    public Guid PassingTestId { get; set; }
    public PassingTest PassingTest { get; set; }
}
