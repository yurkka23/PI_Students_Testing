

using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Test;

public class AddTestModel
{
    [Required]
    [Display(Name = "Name")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 30 characters long")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Description")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "The Description must be between 3 and 500 characters long")]
    public string Description { get; set; } = string.Empty;
    [Required]
    [Display(Name = "StartTime")]
    public DateTimeOffset StartTime { get; set; }
    [Required]
    [Display(Name = "EndTime")]
    public DateTimeOffset EndTime { get; set; }
    [Required]
    [Display(Name = "DurationMinutes")]
    public int DurationMinutes { get; set; }
    [Required]
    public Guid CourseId { get; set; }
}
