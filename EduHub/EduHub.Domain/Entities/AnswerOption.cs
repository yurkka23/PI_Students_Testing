namespace EduHub.Domain.Entities;

public class AnswerOption : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
    public string? AnswerImageUrl { get; set; }
    
    //relations
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }
}
