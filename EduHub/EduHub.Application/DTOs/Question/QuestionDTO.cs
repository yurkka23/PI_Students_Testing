using EduHub.Application.DTOs.Answer;
using EduHub.Domain.Enums;

namespace EduHub.Application.DTOs.Question;

public class QuestionDTO : BaseDTO
{
    public string QuestionContent { get; set; } = string.Empty;
    public string? QuestionImageUrl { get; set; }
    public int Points { get; set; }
    public QuestionType Type { get; set; }
    public ICollection<AnswerOptionDTO>? Answers { get; set; }
}
