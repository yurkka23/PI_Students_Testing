using EduHub.Domain.Enums;

namespace EduHub.Domain.Entities;

public class Question : BaseEntity
{
    public string QuestionContent { get; set; }
    public string? QuestionImageUrl { get; set; }
    public int Points { get; set; }
    public QuestionType Type { get; set; }

    public Guid RightAnswerId { get; set; }
    public AnswerOption RightAnswer { get; set; }
    public ICollection<AnswerOption> Answers { get; set; }
}
