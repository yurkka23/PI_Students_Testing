
using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Answer;

public class AddAnswerModel
{
    [Required]
    [Display(Name = "Content")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "The Content must be between 3 and 30 characters long")]
    public string Content { get; set; } = string.Empty;
    [Required]
    [Display(Name = "IsCorrect")]
    public bool IsCorrect { get; set; }
   
    [Required]
    public Guid QuestionId { get; set; }
    [Required]
    public Guid TestId { get; set; }
}
