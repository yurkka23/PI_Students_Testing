using EduHub.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Question;

public class AddQuestionModel
{
    [Required]
    [Display(Name = "QuestionContent")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "The Question must be between 3 and 100 characters long")]
    public string QuestionContent { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Points")]
    public int Points { get; set; }
    [Required]
    [Display(Name = "Type")]
    public QuestionType Type { get; set; }
    [Required]
    [Display(Name = "TestId")]
    public Guid TestId { get; set; }
}
