using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Course;

    public class CreateCourseModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(64, MinimumLength = 3, ErrorMessage = "The Name must be between 3 and 64 characters long")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Description")]
        [StringLength(1000, MinimumLength = 3,
            ErrorMessage = "The Description must be between 3 and 1000 characters long")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Password")] public string? Password { get; set; }
    }
