using System.ComponentModel.DataAnnotations;

namespace EduHub.Application.ViewModels.Auth;

public class RegisterModel
{
    [Required]
    [Display(Name = "UserName")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The UserName must be between 3 and 10 characters long")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "FirstName")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The FirstName must be between 3 and 10 characters long")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "LastName")]
    [StringLength(10, MinimumLength = 3, ErrorMessage = "The LastName must be between 3 and 10 characters long")]
    public string LastName { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    public string PasswordConfirm { get; set; }

}
