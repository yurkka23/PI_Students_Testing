namespace EduHub.Domain.Entities;

public class PassingTest: BaseEntity
{
    public Guid StudentId { get; set; }
    public Guid TestId { get; set; }
    public DateTimeOffset? StudentStartedAt { get; set; }
    public DateTimeOffset? StudentFinishedAt { get; set; }
    public ICollection<QuestionAnswer> QuestionsAnswers { get; set; }
}
