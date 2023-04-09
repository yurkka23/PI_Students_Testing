using EduHub.Domain.Enums;

namespace EduHub.Domain.Entities;

public class Question : BaseEntity
{
    public string QuestionContent { get; set; } = string.Empty;
    public string? QuestionImageUrl { get; set; }
    public int Points { get; set; }
    public QuestionType Type { get; set; }

    //relations
    public Guid TestId { get; set; }
    public Test Test { get; set; }
    public ICollection<AnswerOption> Answers { get; set; }
}
