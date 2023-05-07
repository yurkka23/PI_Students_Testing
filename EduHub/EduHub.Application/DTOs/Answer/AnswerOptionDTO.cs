namespace EduHub.Application.DTOs.Answer
{
    public class AnswerOptionDTO : BaseDTO
    {
        public string Content { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public string? AnswerImageUrl { get; set; }
    }
}