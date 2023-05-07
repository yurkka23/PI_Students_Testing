using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.Models.Profile
{
    public class EditProfileModel
    {
        [Required]
        [Display(Name = "UserName")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The UserName must be between 3 and 10 characters long")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "FirstName")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The FirstName must be between 3 and 10 characters long")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "LastName")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The LastName must be between 3 and 10 characters long")]
        public string LastName { get; set; } = string.Empty;

        [Display(Name = "AboutMe")] public string? AboutMe { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;
    }
}